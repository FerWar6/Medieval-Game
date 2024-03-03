using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

public class ResizeWindow : MonoBehaviour
{

    public enum WindowModes { Fullscreen, Borderless, Windowed }

    //Import window changing function
    [DllImport("USER32.DLL")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    //Import find window function
    [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
    static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

    //Import force window draw function
    [DllImport("user32.dll")]
    static extern bool DrawMenuBar(IntPtr hWnd);

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    private readonly string WINDOW_NAME = "FullscreenTest";            //name of the window
    private const int GWL_STYLE = -16;              //hex constant for style changing
    private const int WS_BORDER = 0x00800000;       //window with border
    private const int WS_CAPTION = 0x00C00000;      //window with a title bar
    private const int WS_SYSMENU = 0x00080000;      //window with no borders etc.
    private const int WS_MINIMIZEBOX = 0x00020000;  //window with minimizebox
    private const int SWP_SHOWWINDOW = 0x0040;      //displays the window


    public void ChangeWindowMode(WindowModes Mode, int width, int height)
    {
        switch (Mode)
        {
            case WindowModes.Fullscreen:
                Screen.SetResolution(width, height, true);
                break;
            case WindowModes.Borderless:
                Screen.SetResolution(width, height, false);
                BorderlessWindowed(width, height);
                break;
            case WindowModes.Windowed:
                Screen.SetResolution(width, height, false);
                WindowedMode(width, height);
                break;
        }
    }

    public void BorderlessWindowed(int _width, int _height)
    {
        IntPtr window = FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
        SetWindowLong(window, GWL_STYLE, WS_SYSMENU);
        //next line breaks fullscreen -> borderless, coming from windowed works correctly
        SetWindowPos(window, -2, 0, 0, _width, _height, SWP_SHOWWINDOW);
        DrawMenuBar(window);
    }

    public void WindowedMode(int _width, int _height)
    {
        IntPtr window = FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
        SetWindowLong(window, GWL_STYLE, WS_CAPTION | WS_BORDER | WS_SYSMENU | WS_MINIMIZEBOX);
        //next line breaks fullscreen -> windowed
        //SetWindowPos(window, -2, 0, 0, _width, _height, SWP_SHOWWINDOW);
        DrawMenuBar(window);
    }
}
