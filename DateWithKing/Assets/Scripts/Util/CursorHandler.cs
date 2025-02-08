using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 커서를 변환하는 데 사용됩니다.
/// </summary>
public static class CursorHandler
{
    private static Texture2D beforeCursorImage = null;
    /// <summary>
    /// 커서를 바꾸는 함수 <br/>
    /// cursorImage를 비워둘 시 default 커서로 돌아감
    /// </summary>
    /// <param name="cursorImage"> 바꿀 커서 이미지 </param>
    public static void ChangeCursor(Texture2D cursorImage = null)
    {
        if (cursorImage != null) beforeCursorImage = cursorImage;
        Vector2 hotspot = Vector2.zero;
        if(cursorImage is not null)
            hotspot = new Vector2(cursorImage.width / 2f, cursorImage.height / 2f);
        Cursor.SetCursor(cursorImage, hotspot, CursorMode.Auto);
    }

    /// <summary>
    /// (null을 제외한) 직전 커서 이미지로 커서를 바꿈
    /// </summary>
    public static void ReturnCursor()
    {

        Vector2 hotspot = Vector2.zero;
        if(beforeCursorImage is not null)
            hotspot = new Vector2(beforeCursorImage.width / 2f, beforeCursorImage.height / 2f);
        Cursor.SetCursor(beforeCursorImage, Vector2.zero, CursorMode.Auto);
    }

    /// <summary>
    /// 디폴트 커서로 돌려놓음
    /// </summary>
    public static void DefaultCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
