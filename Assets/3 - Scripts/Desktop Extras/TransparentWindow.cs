/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI; 

public class TransparentWindow : MonoBehaviour {

    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    private struct MARGINS {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    const int GWL_EXSTYLE = -20;

    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    const uint LWA_COLORKEY = 0x00000001;
    
    private IntPtr hWnd;
    public LayerMask layer;
    bool isActive;

    private void Awake()
    {
      
       // localButton.onClick.AddListener(methodB);
    }

    private void Start() {
        //MessageBox(new IntPtr(0), "Hello World!", "Hello Dialog", 0);

#if !UNITY_EDITOR
        hWnd = GetActiveWindow();

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        //SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);

        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
#endif
        Application.runInBackground = true;
    }

    private void FixedUpdate() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool result = Physics.Raycast(ray, out RaycastHit hit, 1000, layer); 
        SetClickthrough(result);
        print("Raycast Result: " + result);
        // SetClickthrough(Physics2D.OverlapPoint(CodeMonkey.Utils.UtilsClass.GetMouseWorldPosition()) == null);
    }

    private void SetClickthrough(bool clickThrough) {
        if (clickThrough) {
            if (!isActive)
            {
                SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
                print("SET TRANSPARENT"); 
                isActive = true; 
            }
        } else {
            if (isActive)
            {
                SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
               
                print("SET Not TRANSPARENT");
                isActive = false; 
            }
        }
    }
}