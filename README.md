## What is GLFW?
[GLFW](http://www.glfw.org/) is an Open Source, multi-platform library for creating windows with OpenGL contexts and receiving input and events. It is easy to integrate into existing applications and does not lay claim to the main loop.

## What is GLFW-CS?
This is a set of C# bindings for GLFW, so if you're a C# programmer, you can use it as an alternative to SDL2, OpenTK, or SFML. Those are all great libraries, but I personally like using GLFW so I decided to write this.

## Example Code
Here is a port of the [example code](http://www.glfw.org/documentation.html) from the GLFW documentation:

```csharp
using Glfw3;

public class Example
{
    public static void Main(string[] args)
    {
        /* Initialize the library */
        if (!Glfw.Init())
            return;

        /* Create a windowed mode window and its OpenGL context */
        Glfw.Window window = Glfw.CreateWindow(640, 480, "Hello World");
        if (!window)
        {
            Glfw.Terminate();
            return;
        }

        /* Make the window's context current */
        Glfw.MakeContextCurrent(window);

        /* Loop until the user closes the window */
        while (!Glfw.WindowShouldClose(window))
        {
            /* Render here */

            /* Swap front and back buffers */
            Glfw.SwapBuffers(window);

            /* Poll for and process events */
            Glfw.PollEvents();
        }

        Glfw.Terminate();
    }
}

```

## OpenGL Rendering
GLFW can be used to create a window, handle input and events, and manage OpenGL contexts, but it does not actually come with OpenGL bindings. I am currently using [OpenGL.Net](https://github.com/luca-piccioni/OpenGL.Net) for bindings right now, which seems to work well. I will probably write a tutorial on setting up the whole process on different platforms later on.
