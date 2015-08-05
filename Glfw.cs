using System;
using System.Runtime.InteropServices;

namespace Glfw3
{
    public delegate void ErrorFunc(ErrorCode code, [MarshalAs(UnmanagedType.LPStr)] string desc);
    public delegate void MonitorFunc(Monitor monitor, MonitorEvent ev);

    public enum ErrorCode
    {
        NotInitialized = 0x00010001,
        NoCurrentContext = 0x00010002,
        InvalidEnum = 0x00010003,
        InvalidValue = 0x00010004,
        OutOfMemory = 0x00010005,
        ApiUnavailable = 0x00010006,
        VersionUnavailable = 0x00010007,
        PlatformError = 0x00010008,
        FormatUnavailable = 0x00010009
    }

    public enum MonitorEvent
    {
        Connected = 0x00040001,
        Disconnected = 0x00040002
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Monitor
    {
        public IntPtr Ptr { get; private set; }

        internal Monitor(IntPtr ptr)
        {
            Ptr = ptr;
        }

        public override bool Equals(object obj)
        {
            if (obj is Monitor)
                return Equals((Monitor)obj);
            return obj == null && Ptr == IntPtr.Zero;
        }
        public bool Equals(Monitor obj)
        {
            return Ptr == obj.Ptr;
        }

        public override int GetHashCode()
        {
            return Ptr.GetHashCode();
        }

        public static bool operator ==(Monitor a, Monitor b)
        {
            return a.Equals(b);
        }
        public static bool operator ==(Monitor a, object b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Monitor a, Monitor b)
        {
            return !a.Equals(b);
        }
        public static bool operator !=(Monitor a, object b)
        {
            return a.Equals(b);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VideoMode
    {
        public int Width;
        public int Height;
        public int RedBits;
        public int GreenBits;
        public int BlueBits;
        public int RefreshRate;

        public VideoMode(int width, int height, int redBits, int greenBits, int blueBits, int refreshRate)
        {
            Width = width;
            Height = height;
            RedBits = redBits;
            GreenBits = greenBits;
            BlueBits = blueBits;
            RefreshRate = refreshRate;
        }
    }

    public static class Glfw
    {
        internal const string dll = "libglfw.3.1";

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern int glfwInit();
        public static bool Init()
        {
            return glfwInit() == 1;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "glfwTerminate")]
        public static extern void Terminate();

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static unsafe extern void glfwGetVersion(int* major, int* minor, int* rev);
        public static unsafe void GetVersion(out int major, out int minor, out int rev)
        {
            int a, b, c;
            glfwGetVersion(&a, &b, &c);
            major = a;
            minor = b;
            rev = c;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr glfwGetVersionString();
        public static unsafe string GetVersionString()
        {
            IntPtr version = glfwGetVersionString();
            return Marshal.PtrToStringAnsi(version);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr glfwSetErrorCallback(IntPtr callback);
        public static void SetErrorCallback(ErrorFunc callback)
        {
            IntPtr ptr = callback != null ? Marshal.GetFunctionPointerForDelegate(callback) : IntPtr.Zero;
            glfwSetErrorCallback(ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe IntPtr glfwGetMonitors(int* count);
        public static unsafe Monitor[] GetMonitors()
        {
            int count;
            var ptr = glfwGetMonitors(&count);
            ptr = Marshal.PtrToStructure<IntPtr>(ptr);
            var monitors = new Monitor[count];
            for (int i = 0; i < count; ++i)
            {
                monitors[i] = new Monitor(ptr);
                ptr = IntPtr.Add(ptr, 1);
            }
            return monitors;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr glfwGetPrimaryMonitor();
        public static Monitor GetPrimaryMonitor()
        {
            IntPtr ptr = glfwGetPrimaryMonitor();
            return new Monitor(ptr);
        }

        [DllImport(Glfw.dll, CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe void glfwGetMonitorPos(IntPtr monitor, int* xpos, int* ypos);
        public static unsafe void GetMonitorPosition(Monitor monitor, out int x, out int y)
        {
            int xx, yy;
            glfwGetMonitorPos(monitor.Ptr, &xx, &yy);
            x = xx; y = yy;
        }

        [DllImport(Glfw.dll, CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe void glfwGetMonitorPhysicalSize(IntPtr monitor, int* w, int* h);
        public static unsafe void GetMonitorPhysicalSize(Monitor monitor, out int w, out int h)
        {
            int ww, hh;
            glfwGetMonitorPhysicalSize(monitor.Ptr, &ww, &hh);
            w = ww; h = hh;
        }

        [DllImport(Glfw.dll, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr glfwGetMonitorName(IntPtr monitor);
        public static unsafe string GetMonitortName(Monitor monitor)
        {
            IntPtr name = glfwGetMonitorName(monitor.Ptr);
            return Marshal.PtrToStringAnsi(name);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr glfwSetMonitorCallback(IntPtr callback);
        public static void SetMonitorCallback(MonitorFunc callback)
        {
            if (callback != null)
            {
                var call = new Action<IntPtr, int>((m, e) => callback(new Monitor(m), (MonitorEvent)e));
                var ptr = Marshal.GetFunctionPointerForDelegate(call);
                glfwSetMonitorCallback(ptr);
            }
            else
                glfwSetMonitorCallback(IntPtr.Zero);
        }

        [DllImport(Glfw.dll, CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe IntPtr glfwGetVideoModes(IntPtr monitor, int* count);
        public static unsafe VideoMode[] GetVideoModes(Monitor monitor)
        {
            int count;
            IntPtr ptr = glfwGetVideoModes(monitor.Ptr, &count);
            var modes = new VideoMode[count];
            for (int i = 0; i < count; ++i)
            {
                modes[i] = Marshal.PtrToStructure<VideoMode>(ptr);
                ptr = IntPtr.Add(ptr, 1);
            }
            return modes;
        }
    }
}

