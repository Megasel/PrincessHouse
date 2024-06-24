// ILSpyBased#2
using UnityEngine;

public static class tk2dTextGeomGen
{
    public class GeomData
    {
        internal tk2dTextMeshData textMeshData;

        internal tk2dFontData fontInst;

        internal string formattedText = string.Empty;
    }

    private static GeomData tmpData = new GeomData();

    private static readonly Color32[] channelSelectColors = new Color32[4] {
        new Color32(0, 0, 255, 0),
        new Color(0f, 255f, 0f, 0f),
        new Color(255f, 0f, 0f, 0f),
        new Color(0f, 0f, 0f, 255f)
    };

    private static Color32 meshTopColor = new Color32(255, 255, 255, 255);

    private static Color32 meshBottomColor = new Color32(255, 255, 255, 255);

    private static float meshGradientTexU = 0f;

    private static int curGradientCount = 1;

    private static Color32 errorColor = new Color32(255, 0, 255, 255);

    public static GeomData Data(tk2dTextMeshData textMeshData, tk2dFontData fontData, string formattedText)
    {
        tk2dTextGeomGen.tmpData.textMeshData = textMeshData;
        tk2dTextGeomGen.tmpData.fontInst = fontData;
        tk2dTextGeomGen.tmpData.formattedText = formattedText;
        return tk2dTextGeomGen.tmpData;
    }

    public static Vector2 GetMeshDimensionsForString(string str, GeomData geomData)
    {
        tk2dTextMeshData textMeshData = geomData.textMeshData;
        tk2dFontData fontInst = geomData.fontInst;
        float b = 0f;
        float num = 0f;
        float num2 = 0f;
        bool flag = false;
        int num3 = 0;
        for (int i = 0; i < str.Length && num3 < textMeshData.maxChars; i++)
        {
            if (flag)
            {
                flag = false;
            }
            else
            {
                int num4 = str[i];
                if (num4 == 10)
                {
                    b = Mathf.Max(num, b);
                    num = 0f;
                    num2 -= (fontInst.lineHeight + textMeshData.lineSpacing) * textMeshData.scale.y;
                }
                else
                {
                    if (textMeshData.inlineStyling && num4 == 94 && i + 1 < str.Length)
                    {
                        if (str[i + 1] != '^')
                        {
                            int num5 = 0;
                            switch (str[i + 1])
                            {
                                case 'c':
                                    num5 = 5;
                                    break;
                                case 'C':
                                    num5 = 9;
                                    break;
                                case 'g':
                                    num5 = 9;
                                    break;
                                case 'G':
                                    num5 = 17;
                                    break;
                            }
                            i += num5;
                            continue;
                        }
                        flag = true;
                    }
                    bool flag2 = num4 == 94;
                    tk2dFontChar tk2dFontChar;
                    if (fontInst.useDictionary)
                    {
                        if (!fontInst.charDict.ContainsKey(num4))
                        {
                            num4 = 0;
                        }
                        tk2dFontChar = fontInst.charDict[num4];
                    }
                    else
                    {
                        if (num4 >= fontInst.chars.Length)
                        {
                            num4 = 0;
                        }
                        tk2dFontChar = fontInst.chars[num4];
                    }
                    if (flag2)
                    {
                        num4 = 94;
                    }
                    num += (tk2dFontChar.advance + textMeshData.spacing) * textMeshData.scale.x;
                    if (textMeshData.kerning && i < str.Length - 1)
                    {
                        tk2dFontKerning[] kerning = fontInst.kerning;
                        foreach (tk2dFontKerning tk2dFontKerning in kerning)
                        {
                            if (tk2dFontKerning.c0 == str[i] && tk2dFontKerning.c1 == str[i + 1])
                            {
                                num += tk2dFontKerning.amount * textMeshData.scale.x;
                                break;
                            }
                        }
                    }
                    num3++;
                }
            }
        }
        b = Mathf.Max(num, b);
        num2 -= (fontInst.lineHeight + textMeshData.lineSpacing) * textMeshData.scale.y;
        return new Vector2(b, num2);
    }

    public static float GetYAnchorForHeight(float textHeight, GeomData geomData)
    {
        tk2dTextMeshData textMeshData = geomData.textMeshData;
        tk2dFontData fontInst = geomData.fontInst;
        int num = (int)textMeshData.anchor / 3;
        float num2 = (fontInst.lineHeight + textMeshData.lineSpacing) * textMeshData.scale.y;
        switch (num)
        {
            case 0:
                return 0f - num2;
            case 1:
                {
                    float num3 = (0f - textHeight) / 2f - num2;
                    if (fontInst.version >= 2)
                    {
                        float num4 = fontInst.texelSize.y * textMeshData.scale.y;
                        return Mathf.Floor(num3 / num4) * num4;
                    }
                    return num3;
                }
            case 2:
                return 0f - textHeight - num2;
            default:
                return 0f - num2;
        }
    }

    public static float GetXAnchorForWidth(float lineWidth, GeomData geomData)
    {
        tk2dTextMeshData textMeshData = geomData.textMeshData;
        tk2dFontData fontInst = geomData.fontInst;
        switch ((int)textMeshData.anchor % 3)
        {
            case 0:
                return 0f;
            case 1:
                {
                    float num = (0f - lineWidth) / 2f;
                    if (fontInst.version >= 2)
                    {
                        float num2 = fontInst.texelSize.x * textMeshData.scale.x;
                        return Mathf.Floor(num / num2) * num2;
                    }
                    return num;
                }
            case 2:
                return 0f - lineWidth;
            default:
                return 0f;
        }
    }

    private static void PostAlignTextData(Vector3[] pos, int offset, int targetStart, int targetEnd, float offsetX)
    {
        for (int i = targetStart * 4; i < targetEnd * 4; i++)
        {
            Vector3 vector = pos[offset + i];
            vector.x += offsetX;
            pos[offset + i] = vector;
        }
    }

    private static int GetFullHexColorComponent(int c1, int c2)
    {
        int num = 0;
        if (c1 >= 48 && c1 <= 57)
        {
            num += (c1 - 48) * 16;
            goto IL_0067;
        }
        if (c1 >= 97 && c1 <= 102)
        {
            num += (10 + c1 - 97) * 16;
            goto IL_0067;
        }
        if (c1 >= 65 && c1 <= 70)
        {
            num += (10 + c1 - 65) * 16;
            goto IL_0067;
        }
        return -1;
    IL_0067:
        if (c2 >= 48 && c2 <= 57)
        {
            return num + (c2 - 48);
        }
        if (c2 >= 97 && c2 <= 102)
        {
            return num + (10 + c2 - 97);
        }
        if (c2 >= 65 && c2 <= 70)
        {
            return num + (10 + c2 - 65);
        }
        return -1;
    }

    private static int GetCompactHexColorComponent(int c)
    {
        if (c >= 48 && c <= 57)
        {
            return (c - 48) * 17;
        }
        if (c >= 97 && c <= 102)
        {
            return (10 + c - 97) * 17;
        }
        if (c >= 65 && c <= 70)
        {
            return (10 + c - 65) * 17;
        }
        return -1;
    }

    private static int GetStyleHexColor(string str, bool fullHex, ref Color32 color)
    {
        int num;
        int num2;
        int num3;
        int num4;
        if (fullHex)
        {
            if (str.Length < 8)
            {
                return 1;
            }
            num = tk2dTextGeomGen.GetFullHexColorComponent(str[0], str[1]);
            num2 = tk2dTextGeomGen.GetFullHexColorComponent(str[2], str[3]);
            num3 = tk2dTextGeomGen.GetFullHexColorComponent(str[4], str[5]);
            num4 = tk2dTextGeomGen.GetFullHexColorComponent(str[6], str[7]);
        }
        else
        {
            if (str.Length < 4)
            {
                return 1;
            }
            num = tk2dTextGeomGen.GetCompactHexColorComponent(str[0]);
            num2 = tk2dTextGeomGen.GetCompactHexColorComponent(str[1]);
            num3 = tk2dTextGeomGen.GetCompactHexColorComponent(str[2]);
            num4 = tk2dTextGeomGen.GetCompactHexColorComponent(str[3]);
        }
        if (num != -1 && num2 != -1 && num3 != -1 && num4 != -1)
        {
            color = new Color32((byte)num, (byte)num2, (byte)num3, (byte)num4);
            return 0;
        }
        return 1;
    }

    private static int SetColorsFromStyleCommand(string args, bool twoColors, bool fullHex)
    {
        int num = ((!twoColors) ? 1 : 2) * ((!fullHex) ? 4 : 8);
        bool flag = false;
        if (args.Length >= num)
        {
            if (tk2dTextGeomGen.GetStyleHexColor(args, fullHex, ref tk2dTextGeomGen.meshTopColor) != 0)
            {
                flag = true;
            }
            if (twoColors)
            {
                string str = args.Substring((!fullHex) ? 4 : 8);
                if (tk2dTextGeomGen.GetStyleHexColor(str, fullHex, ref tk2dTextGeomGen.meshBottomColor) != 0)
                {
                    flag = true;
                }
            }
            else
            {
                tk2dTextGeomGen.meshBottomColor = tk2dTextGeomGen.meshTopColor;
            }
        }
        else
        {
            flag = true;
        }
        if (flag)
        {
            tk2dTextGeomGen.meshBottomColor = tk2dTextGeomGen.errorColor; tk2dTextGeomGen.meshTopColor = (tk2dTextGeomGen.meshBottomColor);
        }
        return num;
    }

    private static void SetGradientTexUFromStyleCommand(int arg)
    {
        tk2dTextGeomGen.meshGradientTexU = (float)(arg - 48) / (float)((tk2dTextGeomGen.curGradientCount <= 0) ? 1 : tk2dTextGeomGen.curGradientCount);
    }

    private static int HandleStyleCommand(string cmd)
    {
        if (cmd.Length == 0)
        {
            return 0;
        }
        int num = cmd[0];
        string args = cmd.Substring(1);
        int result = 0;
        switch (num)
        {
            case 99:
                result = 1 + tk2dTextGeomGen.SetColorsFromStyleCommand(args, false, false);
                break;
            case 67:
                result = 1 + tk2dTextGeomGen.SetColorsFromStyleCommand(args, false, true);
                break;
            case 103:
                result = 1 + tk2dTextGeomGen.SetColorsFromStyleCommand(args, true, false);
                break;
            case 71:
                result = 1 + tk2dTextGeomGen.SetColorsFromStyleCommand(args, true, true);
                break;
        }
        if (num >= 48 && num <= 57)
        {
            tk2dTextGeomGen.SetGradientTexUFromStyleCommand(num);
            result = 1;
        }
        return result;
    }

    public static void GetTextMeshGeomDesc(out int numVertices, out int numIndices, GeomData geomData)
    {
        tk2dTextMeshData textMeshData = geomData.textMeshData;
        numVertices = textMeshData.maxChars * 4;
        numIndices = textMeshData.maxChars * 6;
    }

    public static int SetTextMeshGeom(Vector3[] pos, Vector2[] uv, Vector2[] uv2, Color32[] color, int offset, GeomData geomData)
    {
        tk2dTextMeshData textMeshData = geomData.textMeshData;
        tk2dFontData fontInst = geomData.fontInst;
        string formattedText = geomData.formattedText;
        tk2dTextGeomGen.meshTopColor = new Color32(255, 255, 255, 255);
        tk2dTextGeomGen.meshBottomColor = new Color32(255, 255, 255, 255);
        tk2dTextGeomGen.meshGradientTexU = (float)textMeshData.textureGradient / (float)((fontInst.gradientCount <= 0) ? 1 : fontInst.gradientCount);
        tk2dTextGeomGen.curGradientCount = fontInst.gradientCount;
        Vector2 meshDimensionsForString = tk2dTextGeomGen.GetMeshDimensionsForString(geomData.formattedText, geomData);
        float yAnchorForHeight = tk2dTextGeomGen.GetYAnchorForHeight(meshDimensionsForString.y, geomData);
        float num = 0f;
        float num2 = 0f;
        int num3 = 0;
        int num4 = 0;
        for (int i = 0; i < formattedText.Length && num3 < textMeshData.maxChars; i++)
        {
            int num5 = formattedText[i];
            bool flag = num5 == 94;
            tk2dFontChar tk2dFontChar;
            if (fontInst.useDictionary)
            {
                if (!fontInst.charDict.ContainsKey(num5))
                {
                    num5 = 0;
                }
                tk2dFontChar = fontInst.charDict[num5];
            }
            else
            {
                if (num5 >= fontInst.chars.Length)
                {
                    num5 = 0;
                }
                tk2dFontChar = fontInst.chars[num5];
            }
            if (flag)
            {
                num5 = 94;
            }
            if (num5 == 10)
            {
                float lineWidth = num;
                int targetEnd = num3;
                if (num4 != num3)
                {
                    float xAnchorForWidth = tk2dTextGeomGen.GetXAnchorForWidth(lineWidth, geomData);
                    tk2dTextGeomGen.PostAlignTextData(pos, offset, num4, targetEnd, xAnchorForWidth);
                }
                num4 = num3;
                num = 0f;
                num2 -= (fontInst.lineHeight + textMeshData.lineSpacing) * textMeshData.scale.y;
            }
            else
            {
                if (textMeshData.inlineStyling && num5 == 94)
                {
                    if (i + 1 >= formattedText.Length || formattedText[i + 1] != '^')
                    {
                        i += tk2dTextGeomGen.HandleStyleCommand(formattedText.Substring(i + 1));
                        continue;
                    }
                    i++;
                }
                pos[offset + num3 * 4] = new Vector3(num + tk2dFontChar.p0.x * textMeshData.scale.x, yAnchorForHeight + num2 + tk2dFontChar.p0.y * textMeshData.scale.y, 0f);
                pos[offset + num3 * 4 + 1] = new Vector3(num + tk2dFontChar.p1.x * textMeshData.scale.x, yAnchorForHeight + num2 + tk2dFontChar.p0.y * textMeshData.scale.y, 0f);
                pos[offset + num3 * 4 + 2] = new Vector3(num + tk2dFontChar.p0.x * textMeshData.scale.x, yAnchorForHeight + num2 + tk2dFontChar.p1.y * textMeshData.scale.y, 0f);
                pos[offset + num3 * 4 + 3] = new Vector3(num + tk2dFontChar.p1.x * textMeshData.scale.x, yAnchorForHeight + num2 + tk2dFontChar.p1.y * textMeshData.scale.y, 0f);
                if (tk2dFontChar.flipped)
                {
                    uv[offset + num3 * 4] = new Vector2(tk2dFontChar.uv1.x, tk2dFontChar.uv1.y);
                    uv[offset + num3 * 4 + 1] = new Vector2(tk2dFontChar.uv1.x, tk2dFontChar.uv0.y);
                    uv[offset + num3 * 4 + 2] = new Vector2(tk2dFontChar.uv0.x, tk2dFontChar.uv1.y);
                    uv[offset + num3 * 4 + 3] = new Vector2(tk2dFontChar.uv0.x, tk2dFontChar.uv0.y);
                }
                else
                {
                    uv[offset + num3 * 4] = new Vector2(tk2dFontChar.uv0.x, tk2dFontChar.uv0.y);
                    uv[offset + num3 * 4 + 1] = new Vector2(tk2dFontChar.uv1.x, tk2dFontChar.uv0.y);
                    uv[offset + num3 * 4 + 2] = new Vector2(tk2dFontChar.uv0.x, tk2dFontChar.uv1.y);
                    uv[offset + num3 * 4 + 3] = new Vector2(tk2dFontChar.uv1.x, tk2dFontChar.uv1.y);
                }
                if (fontInst.textureGradients)
                {
                    uv2[offset + num3 * 4] = tk2dFontChar.gradientUv[0] + new Vector2(tk2dTextGeomGen.meshGradientTexU, 0f);
                    uv2[offset + num3 * 4 + 1] = tk2dFontChar.gradientUv[1] + new Vector2(tk2dTextGeomGen.meshGradientTexU, 0f);
                    uv2[offset + num3 * 4 + 2] = tk2dFontChar.gradientUv[2] + new Vector2(tk2dTextGeomGen.meshGradientTexU, 0f);
                    uv2[offset + num3 * 4 + 3] = tk2dFontChar.gradientUv[3] + new Vector2(tk2dTextGeomGen.meshGradientTexU, 0f);
                }
                if (fontInst.isPacked)
                {
                    Color32 color2 = tk2dTextGeomGen.channelSelectColors[tk2dFontChar.channel];
                    color[offset + num3 * 4] = color2;
                    color[offset + num3 * 4 + 1] = color2;
                    color[offset + num3 * 4 + 2] = color2;
                    color[offset + num3 * 4 + 3] = color2;
                }
                else
                {
                    color[offset + num3 * 4] = tk2dTextGeomGen.meshTopColor;
                    color[offset + num3 * 4 + 1] = tk2dTextGeomGen.meshTopColor;
                    color[offset + num3 * 4 + 2] = tk2dTextGeomGen.meshBottomColor;
                    color[offset + num3 * 4 + 3] = tk2dTextGeomGen.meshBottomColor;
                }
                num += (tk2dFontChar.advance + textMeshData.spacing) * textMeshData.scale.x;
                if (textMeshData.kerning && i < formattedText.Length - 1)
                {
                    tk2dFontKerning[] kerning = fontInst.kerning;
                    foreach (tk2dFontKerning tk2dFontKerning in kerning)
                    {
                        if (tk2dFontKerning.c0 == formattedText[i] && tk2dFontKerning.c1 == formattedText[i + 1])
                        {
                            num += tk2dFontKerning.amount * textMeshData.scale.x;
                            break;
                        }
                    }
                }
                num3++;
            }
        }
        if (num4 != num3)
        {
            float lineWidth2 = num;
            int targetEnd2 = num3;
            float xAnchorForWidth2 = tk2dTextGeomGen.GetXAnchorForWidth(lineWidth2, geomData);
            tk2dTextGeomGen.PostAlignTextData(pos, offset, num4, targetEnd2, xAnchorForWidth2);
        }
        for (int k = num3; k < textMeshData.maxChars; k++)
        {
            pos[offset + k * 4] = (pos[offset + k * 4 + 1] = (pos[offset + k * 4 + 2] = (pos[offset + k * 4 + 3] = Vector3.zero)));
            uv[offset + k * 4] = (uv[offset + k * 4 + 1] = (uv[offset + k * 4 + 2] = (uv[offset + k * 4 + 3] = Vector2.zero)));
            if (fontInst.textureGradients)
            {
                uv2[offset + k * 4] = (uv2[offset + k * 4 + 1] = (uv2[offset + k * 4 + 2] = (uv2[offset + k * 4 + 3] = Vector2.zero)));
            }
            if (!fontInst.isPacked)
            {
                color[offset + k * 4] = (color[offset + k * 4 + 1] = tk2dTextGeomGen.meshTopColor);
                color[offset + k * 4 + 2] = (color[offset + k * 4 + 3] = tk2dTextGeomGen.meshBottomColor);
            }
            else
            {
                color[offset + k * 4] = (color[offset + k * 4 + 1] = (color[offset + k * 4 + 2] = (color[offset + k * 4 + 3] = Color.clear)));
            }
        }
        return num3;
    }

    public static void SetTextMeshIndices(int[] indices, int offset, int vStart, GeomData geomData, int target)
    {
        tk2dTextMeshData textMeshData = geomData.textMeshData;
        for (int i = 0; i < textMeshData.maxChars; i++)
        {
            indices[offset + i * 6] = vStart + i * 4;
            indices[offset + i * 6 + 1] = vStart + i * 4 + 1;
            indices[offset + i * 6 + 2] = vStart + i * 4 + 3;
            indices[offset + i * 6 + 3] = vStart + i * 4 + 2;
            indices[offset + i * 6 + 4] = vStart + i * 4;
            indices[offset + i * 6 + 5] = vStart + i * 4 + 3;
        }
    }
}


