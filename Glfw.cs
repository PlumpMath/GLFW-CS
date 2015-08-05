using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Glfw3
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate void ErrorFunc(ErrorCode code, string desc);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MonitorFunc([MarshalAs(UnmanagedType.Struct)] Monitor monitor, MonitorEvent ev);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void WindowPosFunc([MarshalAs(UnmanagedType.Struct)] Window window, int x, int y);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void WindowSizeFunc([MarshalAs(UnmanagedType.Struct)] Window window, int width, int height);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void WindowFunc([MarshalAs(UnmanagedType.Struct)] Window window);

    public enum ErrorCode
    {
        NotInitialized      = 0x00010001,
        NoCurrentContext    = 0x00010002,
        InvalidEnum         = 0x00010003,
        InvalidValue        = 0x00010004,
        OutOfMemory         = 0x00010005,
        ApiUnavailable      = 0x00010006,
        VersionUnavailable  = 0x00010007,
        PlatformError       = 0x00010008,
        FormatUnavailable   = 0x00010009
    }

    public enum MonitorEvent
    {
        Connected       = 0x00040001,
        Disconnected    = 0x00040002
    }

    public enum WindowBool
    {
        Focused             = 0x00020001,
        Resizable           = 0x00020003,
        Visible             = 0x00020004,
        Decorated           = 0x00020005,
        AutoIconify         = 0x00020006,
        Floating            = 0x00020007,
        Stereo              = 0x0002100C,
        SrgbCapable         = 0x0002100E,
        DoubleBuffer        = 0x00021010,
        OpenGLForwardCompat = 0x00022006,
        OpenGLDebugContext  = 0x00022007
    }

    public enum WindowInt
    {
        RedBits             = 0x00021001,
        GreenBits           = 0x00021002,
        BlueBits            = 0x00021003,
        AlphaBits           = 0x00021004,
        DepthBits           = 0x00021005,
        StencilBits         = 0x00021006,
        AccumRedBits        = 0x00021007,
        AccumGreenBits      = 0x00021008,
        AccumBlueBits       = 0x00021009,
        AccumAlphaBits      = 0x0002100A,
        AuxBuffers          = 0x0002100B,
        Samples             = 0x0002100D,
        RefreshRate         = 0x0002100F,
        ContextVersionMajor = 0x00022002,
        ContextVersionMinor = 0x00022003
    }

    public enum ClientApi
    {
        OpenGLApi   = 0x00030001,
        OpenGLESApi = 0x00030002
    }

    public enum ContextRobustness
    {
        None                = 0,
        NoResetNotification = 0x00031001,
        LoseContextOnReset  = 0x00031002
    }

    public enum ContextReleaseBehavior
    {
        Any     = 0,
        Flush   = 0x00035001,
        None    = 0x00035002
    }

    public enum OpenGLProfile
    {
        Any     = 0,
        Core    = 0x00032001,
        Compat  = 0x00032002
    }

    public enum WindowAttrib
    {
        Focused     = 0x00020001,
        Iconified   = 0x00020002,
        Resizable   = 0x00020003,
        Visible     = 0x00020004,
        Decorated   = 0x00020005,
        Floating    = 0x00020007
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Monitor
    {
        public static readonly Monitor None = new Monitor(IntPtr.Zero);

        public IntPtr Ptr { get; private set; }

        internal Monitor(IntPtr ptr)
        {
            Ptr = ptr;
        }

        public override bool Equals(object obj)
        {
            if (obj is Monitor)
                return Equals((Monitor)obj);
            return false;
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

        public static bool operator !=(Monitor a, Monitor b)
        {
            return !a.Equals(b);
        }

        public static implicit operator bool(Monitor obj)
        {
            return obj.Ptr != IntPtr.Zero;
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

    [StructLayout(LayoutKind.Sequential)]
    internal struct GammaRamp
    {
        internal IntPtr Red;
        internal IntPtr Green;
        internal IntPtr Blue;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Window
    {
        public static readonly Window None = new Window(IntPtr.Zero);

        public IntPtr Ptr { get; private set; }

        internal Window(IntPtr ptr)
        {
            Ptr = ptr;
        }

        public override bool Equals(object obj)
        {
            if (obj is Window)
                return Equals((Window)obj);
            return false;
        }
        public bool Equals(Window obj)
        {
            return Ptr == obj.Ptr;
        }

        public override int GetHashCode()
        {
            return Ptr.GetHashCode();
        }

        public static bool operator ==(Window a, Window b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Window a, Window b)
        {
            return !a.Equals(b);
        }

        public static implicit operator bool(Window obj)
        {
            return obj.Ptr != IntPtr.Zero;
        }
    }

    public static class Glfw
    {
        internal const string dll = "libglfw.3.1";

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "glfwInit")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Init();

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
            glfwSetErrorCallback(Marshal.GetFunctionPointerForDelegate(callback));
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe IntPtr glfwGetMonitors(int* count);
        public static unsafe Monitor[] GetMonitors()
        {
            int count;
            var array = glfwGetMonitors(&count);
            var monitors = new Monitor[count];
            var size = Marshal.SizeOf<IntPtr>();
            for (int i = 0; i < count; ++i)
            {
                var ptr = Marshal.ReadIntPtr(array, i * size);
                monitors[i] = new Monitor(ptr);
            }
            return monitors;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "glfwGetPrimaryMonitor")]
        [return: MarshalAs(UnmanagedType.Struct)]
        public static extern Monitor GetPrimaryMonitor();

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe void glfwGetMonitorPos(IntPtr monitor, int* xpos, int* ypos);
        public static unsafe void GetMonitorPosition(Monitor monitor, out int x, out int y)
        {
            int xx, yy;
            glfwGetMonitorPos(monitor.Ptr, &xx, &yy);
            x = xx; y = yy;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe void glfwGetMonitorPhysicalSize(IntPtr monitor, int* w, int* h);
        public static unsafe void GetMonitorPhysicalSize(Monitor monitor, out int w, out int h)
        {
            int ww, hh;
            glfwGetMonitorPhysicalSize(monitor.Ptr, &ww, &hh);
            w = ww; h = hh;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
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
            glfwSetMonitorCallback(Marshal.GetFunctionPointerForDelegate(callback));
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe IntPtr glfwGetVideoModes(IntPtr monitor, int* count);
        public static unsafe VideoMode[] GetVideoModes(Monitor monitor)
        {
            int count;
            var array = glfwGetVideoModes(monitor.Ptr, &count);
            var modes = new VideoMode[count];
            var size = Marshal.SizeOf<VideoMode>();
            for (int i = 0; i < count; ++i)
            {
                var ptr = Marshal.ReadIntPtr(array, i * size);
                modes[i] = Marshal.PtrToStructure<VideoMode>(ptr);
            }
            return modes;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr glfwGetVideoMode(IntPtr monitor);
        public static VideoMode GetVideoMode(Monitor monitor)
        {
            var ptr = glfwGetVideoMode(monitor.Ptr);
            return Marshal.PtrToStructure<VideoMode>(ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "glfwSetGamma")]
        public static extern void SetGamma([MarshalAs(UnmanagedType.LPStruct)] Monitor monitor, float gamma);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr glfwGetGammaRamp(IntPtr monitor);
        public static unsafe void GetGammaRamp(Monitor monitor, out ushort[] red, out ushort[] green, out ushort[] blue)
        {
            var ptr = glfwGetGammaRamp(monitor.Ptr);
            var ramp = Marshal.PtrToStructure<GammaRamp>(ptr);
            var r = (ushort*)ramp.Red.ToPointer();
            var g = (ushort*)ramp.Green.ToPointer();
            var b = (ushort*)ramp.Blue.ToPointer();
            red = new ushort[256];
            green = new ushort[256];
            blue = new ushort[256];
            for (int i = 0; i < 256; ++i)
                red[i] = r[i];
            for (int i = 0; i < 256; ++i)
                green[i] = g[i];
            for (int i = 0; i < 256; ++i)
                blue[i] = b[i];
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern void glfwSetGammaRamp(IntPtr monitor, IntPtr ramp);
        public static unsafe void SetGammaRamp(Monitor monitor, ushort[] red, ushort[] green, ushort[] blue)
        {
            var ramp = new GammaRamp();
            ramp.Red = Marshal.UnsafeAddrOfPinnedArrayElement(red, 0);
            ramp.Green = Marshal.UnsafeAddrOfPinnedArrayElement(green, 0);
            ramp.Blue = Marshal.UnsafeAddrOfPinnedArrayElement(blue, 0);
            glfwSetGammaRamp(monitor.Ptr, new IntPtr(&ramp));
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "glfwDefaultWindowHints")]
        public static extern void DefaultWindowHints();

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern void glfwWindowHint(int target, int hint);

        public static void WindowHint(WindowBool target, bool value)
        {
            glfwWindowHint((int)target, value ? 1 : 0);
        }
        public static void WindowHint(WindowInt target, int value)
        {
            if (value < 0)
            {
                if (target == WindowInt.ContextVersionMajor)
                    value = 1;
                else if (target == WindowInt.ContextVersionMinor)
                    value = 0;
                else
                    value = -1;
            }
            glfwWindowHint((int)target, value);
        }
        public static void WindowHint(ClientApi value)
        {
            glfwWindowHint(0x00022001, (int)value);
        }
        public static void WindowHint(ContextRobustness value)
        {
            glfwWindowHint(0x00022005, (int)value);
        }
        public static void WindowHint(ContextReleaseBehavior value)
        {
            glfwWindowHint(0x00022009, (int)value);
        }
        public static void WindowHint(OpenGLProfile value)
        {
            glfwWindowHint(0x00022008, (int)value);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "glfwCreateWindow")]
        [return: MarshalAs(UnmanagedType.Struct)]
        public static extern Window CreateWindow(int width, int height, string title, [MarshalAs(UnmanagedType.Struct)] Monitor monitor, [MarshalAs(UnmanagedType.Struct)] Window share);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "glfwDestroyWindow")]
        public static extern void DestroyWindow([MarshalAs(UnmanagedType.Struct)] Window window);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern int glfwWindowShouldClose(IntPtr window);
        public static bool WindowShouldClose(Window window)
        {
            return glfwWindowShouldClose(window.Ptr) == 1;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern void glfwSetWindowShouldClose(IntPtr window, int value);
        public static void SetWindowShouldClose(Window window, bool value)
        {
            glfwSetWindowShouldClose(window.Ptr, value ? 1 : 0);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        static extern void glfwSetWindowTitle(IntPtr window, StringBuilder title);
        public static void SetWindowTitle(Window window, string title)
        {
            glfwSetWindowTitle(window.Ptr, new StringBuilder(title));
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern void glfwMakeContextCurrent(IntPtr window);
        public static void MakeContextCurrent(Window window)
        {
            glfwMakeContextCurrent(window.Ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe void glfwGetWindowPos(IntPtr window, int* x, int* y);
        public static unsafe void GetWindowPos(Window window, out int x, out int y)
        {
            int xx, yy;
            glfwGetWindowPos(window.Ptr, &xx, &yy);
            x = xx; y = yy;
        }
            
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern void glfwSetWindowPos(IntPtr window, int x, int y);
        public static void SetWindowPos(Window window, int x, int y)
        {
            glfwSetWindowPos(window.Ptr, x, y);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe void glfwGetWindowSize(IntPtr window, int* width, int* height);
        public static unsafe void GetWindowSize(Window window, out int width, out int height)
        {
            int w, h;
            glfwGetWindowSize(window.Ptr, &w, &h);
            width = w; height = h;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern void glfwSetWindowSize(IntPtr window, int width, int height);
        public static void SetWindowSize(Window window, int width, int height)
        {
            glfwSetWindowSize(window.Ptr, width, height);
        }
            
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe void glfwGetFramebufferSize(IntPtr window, int* width, int* height);
        public static unsafe void GetFramebufferSize(Window window, out int width, out int height)
        {
            int w, h;
            glfwGetFramebufferSize(window.Ptr, &w, &h);
            width = w; height = h;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe void glfwGetWindowFrameSize(IntPtr window, int* left, int* top, int* right, int* bottom);
        public static unsafe void GetWindowFrameSize(Window window, out int left, out int top, out int right, out int bottom)
        {
            int l, t, r, b;
            glfwGetWindowFrameSize(window.Ptr, &l, &t, &r, &b);
            left = l; top = t; right = r; bottom = b;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern void glfwIconifyWindow(IntPtr window);
        public static void IconifyWindow(Window window)
        {
            glfwIconifyWindow(window.Ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern void glfwRestoreWindow(IntPtr window);
        public static void RestoreWindow(Window window)
        {
            glfwRestoreWindow(window.Ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern void glfwShowWindow(IntPtr window);
        public static void ShowWindow(Window window)
        {
            glfwShowWindow(window.Ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern void glfwHideWindow(IntPtr window);
        public static void HideWindow(Window window)
        {
            glfwHideWindow(window.Ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr glfwGetWindowMonitor(IntPtr window);
        public static Monitor GetWindowMonitor(Window window)
        {
            var ptr = glfwGetWindowMonitor(window.Ptr);
            return new Monitor(ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern int glfwGetWindowAttrib(IntPtr window, int attrib);
        public static bool GetWindowAttrib(Window window, WindowAttrib attrib)
        {
            return glfwGetWindowAttrib(window.Ptr, (int)attrib) == 1;
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern void glfwSwapBuffers(IntPtr window);
        public static void SwapBuffers(Window window)
        {
            glfwSwapBuffers(window.Ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern void glfwSetWindowUserPointer(IntPtr window, IntPtr ptr);
        public static void SetWindowUserPointer(Window window, IntPtr ptr)
        {
            glfwSetWindowUserPointer(window.Ptr, ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr glfwGetWindowUserPointer(IntPtr window);
        public static IntPtr GetWindowUserPointer(Window window)
        {
            return glfwGetWindowUserPointer(window.Ptr);
        }
            
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr glfwSetWindowPosCallback(IntPtr window, IntPtr callback);
        public static void SetWindowPosCallback(Window window, WindowPosFunc callback)
        {
            var ptr = Marshal.GetFunctionPointerForDelegate(callback);
            glfwSetWindowPosCallback(window.Ptr, ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr glfwSetWindowSizeCallback(IntPtr window, IntPtr callback);
        public static void SetWindowSizeCallback(Window window, WindowSizeFunc callback)
        {
            var ptr = Marshal.GetFunctionPointerForDelegate(callback);
            glfwSetWindowSizeCallback(window.Ptr, ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr glfwSetWindowSizeCallback(IntPtr window, IntPtr callback);
        public static void SetWindowSizeCallback(Window window, WindowSizeFunc callback)
        {
            var ptr = Marshal.GetFunctionPointerForDelegate(callback);
            glfwSetWindowSizeCallback(window.Ptr, ptr);
        }

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "glfwPollEvents")]
        public static extern void PollEvents();
    }
}

