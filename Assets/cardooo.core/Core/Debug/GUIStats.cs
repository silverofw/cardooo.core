using System.Text;
using UnityEngine;
using UnityEngine.Profiling;
/// <summary>
/// GUI stats.
/// 性能统计窗口
/// </summary>
public class GUIStats : MonoBehaviour
{

    Rect windowRect = new Rect(50, 20, 250, 250);
    GUIStyle labelStyle;
    GUIStyle fpsStyle;
    GUIStyle memStyle;
    GUIStyle sysStyle;
    string sysinfo = "";

    bool IsShowMem = false;
    bool IsShowScreen = false;
    bool IsShowSysInfo = false;
    bool IsShowApplicationInfo = false;

    void Start()
    {
        labelStyle = new GUIStyle();
        labelStyle.normal.background = null;
        labelStyle.normal.textColor = new Color(0.0f, 1.0f, 0.0f);
        labelStyle.fontSize = 12;

        fpsStyle = new GUIStyle();
        fpsStyle.normal.background = null;
        fpsStyle.normal.textColor = new Color(1.0f, 0.0f, 0.0f);
        fpsStyle.fontSize = 12;

        memStyle = new GUIStyle();
        memStyle.normal.background = null;
        memStyle.normal.textColor = new Color(1.0f, 0.3f, 0.0f);
        memStyle.fontSize = 12;

        sysStyle = new GUIStyle();
        sysStyle.normal.background = null;
        sysStyle.normal.textColor = new Color(1.0f, 1.0f, 1.0f);
        sysStyle.fontSize = 12;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("設備信息");
        sb.AppendLine("操作系统名稱: " + SystemInfo.operatingSystem);
        sb.AppendLine("處理器名稱: " + SystemInfo.processorType);
        sb.AppendLine("處理器数量: " + SystemInfo.processorCount);
        sb.AppendLine("當前系统内存大小: " + SystemInfo.systemMemorySize + "MB");
        sb.AppendLine("當前顯存大小: " + SystemInfo.graphicsMemorySize + "MB");
        sb.AppendLine("顯卡名字: " + SystemInfo.graphicsDeviceName);
        sb.AppendLine("顯卡廠商: " + SystemInfo.graphicsDeviceVendor);
        sb.AppendLine("顯卡的标識符代碼: " + SystemInfo.graphicsDeviceID);
        sb.AppendLine("顯卡廠商的标識符代碼: " + SystemInfo.graphicsDeviceVendorID);
        sb.AppendLine("該顯卡所支持的圖形API版本: " + SystemInfo.graphicsDeviceVersion);
        sb.AppendLine("圖形設備著色器性能級别: " + SystemInfo.graphicsShaderLevel);
        //sb.AppendLine("顯卡的近似像素填充率: " + SystemInfo.graphicsPixelFillrate);
        sb.AppendLine("是否支持内置陰影: " + SystemInfo.supportsShadows);
        //sb.AppendLine("是否支持渲染纹理: " + SystemInfo.supportsRenderTextures);
        //sb.AppendLine("是否支持圖像效果: " + SystemInfo.supportsImageEffects);
        sb.AppendLine("設備唯一標識符: " + SystemInfo.deviceUniqueIdentifier);
        sb.AppendLine("用戶定義的設備的名稱: " + SystemInfo.deviceName);
        sb.AppendLine("設備的模型: " + SystemInfo.deviceModel);
        sysinfo = sb.ToString();
    }

    private void Update()
    {
        fpsCounter++;
        if (Time.realtimeSinceStartup - lastUpdateFpsTime >= updateFpsInterval)
        {
            FPS = fpsCounter / updateFpsInterval;
            lastUpdateFpsTime = Time.realtimeSinceStartup;
            fpsCounter = 0;
        }
    }

    void OnGUI()
    {
        windowRect.width = 150;
        windowRect.height = 25;
        //宽度和高度根据内容自适应
        windowRect = GUILayout.Window(0, windowRect, DrawWindow, "<color=yellow>Statistics</color>", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
    }

    void DrawWindow(int windowID)
    {
        GUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        DrawFPS();
        if (IsShowMem)
            DrawMem();
        if (IsShowScreen)
            DrawScreen();
        if (IsShowSysInfo)
            DrawSysInfo();
        if (IsShowApplicationInfo) 
            DrawApplicationInfo();
        DrawBottom();
        GUILayout.EndVertical();
        GUI.DragWindow();
    }

    private void DrawBottom()
    {
        GUILayout.BeginHorizontal();
        IsShowMem = GUILayout.Toggle(IsShowMem, "内存信息");
        IsShowScreen = GUILayout.Toggle(IsShowScreen, "屏幕信息");
        IsShowSysInfo = GUILayout.Toggle(IsShowSysInfo, "設備信息");
        IsShowApplicationInfo = GUILayout.Toggle(IsShowApplicationInfo, "項目信息");
        GUILayout.EndHorizontal();
    }

    float FPS;//帧率
    float updateFpsInterval = 1;//更新帧率的间隔
    float fpsCounter;//fps计数器
    float lastUpdateFpsTime;//上一次更新帧率的时间

    private void DrawFPS()
    {
        GUILayout.BeginHorizontal();
        //GUILayout.Label("FPS: " + (int)(1 / Time.deltaTime), fpsStyle);
        GUILayout.Label($"FPS: {FPS}", fpsStyle);

        if (Profiler.supported)
        {
            long TotalAllocatedMemory = Profiler.GetTotalAllocatedMemoryLong() / 1024 / 1024; //"MB"
            int systemMemorySize = SystemInfo.systemMemorySize;
            GUILayout.Label("Mem: " + TotalAllocatedMemory + " / " + systemMemorySize + "MB", fpsStyle);
        }
        GUILayout.EndHorizontal();
    }

    private void DrawMem()
    {
        if (Profiler.supported)
        {
            long TotalReservedMemory = Profiler.GetTotalReservedMemoryLong() / 1024 / 1024; //"MB"
            long TotalAllocatedMemory = Profiler.GetTotalAllocatedMemoryLong() / 1024 / 1024; //"MB"
            long TotalUnusedReservedMemory = Profiler.GetTotalUnusedReservedMemoryLong() / 1024 / 1024; //"MB"
            long MonoHeapSize = (Profiler.GetMonoHeapSizeLong() >> 10) / 1024; //"MB"
            long MonoUsedSize = (Profiler.GetMonoUsedSizeLong() >> 10) / 1024; //"MB"
            long TempAllocatorSize = Profiler.GetTempAllocatorSize() / 1024 / 1024; //"MB"

            GUILayout.Label("TotalReservedMemory: " + TotalReservedMemory + "MB", memStyle);
            GUILayout.Label("TotalAllocatedMemory: " + TotalAllocatedMemory + "MB", memStyle);
            GUILayout.Label("TotalUnusedReservedMemory: " + TotalUnusedReservedMemory + "MB", memStyle);
            GUILayout.Label("MonoHeapSize: " + MonoHeapSize + "MB", memStyle);
            GUILayout.Label("MonoUsedSize: " + MonoUsedSize + "MB", memStyle);
            GUILayout.Label("TempAllocatorSize: " + TempAllocatorSize + "MB", memStyle);
        }
    }

    private void DrawScreen()
    {
        Resolution resolution = Screen.currentResolution;
        GUILayout.Label("Resolution: " + resolution.ToString(), labelStyle);
        GUILayout.Label("Screen: " + Screen.width + "x" + Screen.height, labelStyle);
        GUILayout.Label("dpi: " + Screen.dpi, labelStyle);
        GUILayout.Label("sleepTimeout: " + Screen.sleepTimeout, labelStyle);
    }

    private void DrawSysInfo()
    {
        GUILayout.Box(sysinfo, sysStyle);
    }

    private void DrawApplicationInfo()
    {
        GUILayout.Label("項目名稱：" + Application.productName);
        GUILayout.Label("項目包名：" + Application.identifier);
        GUILayout.Label("項目版本：" + Application.version);
        GUILayout.Label("Unity版本：" + Application.unityVersion);
        GUILayout.Label("公司名稱：" + Application.companyName);
        GUILayout.Label("項目平台：" + Application.platform);
    }
}