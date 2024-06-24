// ILSpyBased#2
using System;
using System.Text;
using tk2dRuntime;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[AddComponentMenu("2D Toolkit/Text/tk2dTextMesh")]
public class tk2dTextMesh : MonoBehaviour, ISpriteCollectionForceBuild
{
    [Flags]
    private enum UpdateFlags
    {
        UpdateNone = 0,
        UpdateText = 1,
        UpdateColors = 2,
        UpdateBuffers = 4
    }

    private tk2dFontData _fontInst;

    private string _formattedText = string.Empty;

    [SerializeField]
    private tk2dFontData _font;

    [SerializeField]
    private string _text = string.Empty;

    [SerializeField]
    private Color _color = Color.white;

    [SerializeField]
    private Color _color2 = Color.white;

    [SerializeField]
    private bool _useGradient;

    [SerializeField]
    private int _textureGradient;

    [SerializeField]
    private TextAnchor _anchor = TextAnchor.LowerLeft;

    [SerializeField]
    private Vector3 _scale = new Vector3(1f, 1f, 1f);

    [SerializeField]
    private bool _kerning;

    [SerializeField]
    private int _maxChars = 16;

    [SerializeField]
    private bool _inlineStyling;

    [SerializeField]
    private bool _formatting;

    [SerializeField]
    private int _wordWrapWidth;

    [SerializeField]
    private float spacing;

    [SerializeField]
    private float lineSpacing;

    [SerializeField]
    private tk2dTextMeshData data = new tk2dTextMeshData();

    private Vector3[] vertices;

    private Vector2[] uvs;

    private Vector2[] uv2;

    private Color32[] colors;

    private Color32[] untintedColors;

    private UpdateFlags updateFlags = UpdateFlags.UpdateBuffers;

    private Mesh mesh;

    private MeshFilter meshFilter;

    private Renderer _cachedRenderer;

    public string FormattedText
    {
        get
        {
            return this._formattedText;
        }
    }

    public tk2dFontData font
    {
        get
        {
            this.UpgradeData();
            return this.data.font;
        }
        set
        {
            this.UpgradeData();
            this.data.font = value;
            this._fontInst = this.data.font.inst;
            this.SetNeedUpdate(UpdateFlags.UpdateText);
            this.UpdateMaterial();
        }
    }

    public bool formatting
    {
        get
        {
            this.UpgradeData();
            return this.data.formatting;
        }
        set
        {
            this.UpgradeData();
            if (this.data.formatting != value)
            {
                this.data.formatting = value;
                this.SetNeedUpdate(UpdateFlags.UpdateText);
            }
        }
    }

    public int wordWrapWidth
    {
        get
        {
            this.UpgradeData();
            return this.data.wordWrapWidth;
        }
        set
        {
            this.UpgradeData();
            if (this.data.wordWrapWidth != value)
            {
                this.data.wordWrapWidth = value;
                this.SetNeedUpdate(UpdateFlags.UpdateText);
            }
        }
    }

    public string text
    {
        get
        {
            this.UpgradeData();
            return this.data.text;
        }
        set
        {
            this.UpgradeData();
            this.data.text = value;
            this.SetNeedUpdate(UpdateFlags.UpdateText);
        }
    }

    public Color color
    {
        get
        {
            this.UpgradeData();
            return this.data.color;
        }
        set
        {
            this.UpgradeData();
            this.data.color = value;
            this.SetNeedUpdate(UpdateFlags.UpdateColors);
        }
    }

    public Color color2
    {
        get
        {
            this.UpgradeData();
            return this.data.color2;
        }
        set
        {
            this.UpgradeData();
            this.data.color2 = value;
            this.SetNeedUpdate(UpdateFlags.UpdateColors);
        }
    }

    public bool useGradient
    {
        get
        {
            this.UpgradeData();
            return this.data.useGradient;
        }
        set
        {
            this.UpgradeData();
            this.data.useGradient = value;
            this.SetNeedUpdate(UpdateFlags.UpdateColors);
        }
    }

    public TextAnchor anchor
    {
        get
        {
            this.UpgradeData();
            return this.data.anchor;
        }
        set
        {
            this.UpgradeData();
            this.data.anchor = value;
            this.SetNeedUpdate(UpdateFlags.UpdateText);
        }
    }

    public Vector3 scale
    {
        get
        {
            this.UpgradeData();
            return this.data.scale;
        }
        set
        {
            this.UpgradeData();
            this.data.scale = value;
            this.SetNeedUpdate(UpdateFlags.UpdateText);
        }
    }

    public bool kerning
    {
        get
        {
            this.UpgradeData();
            return this.data.kerning;
        }
        set
        {
            this.UpgradeData();
            this.data.kerning = value;
            this.SetNeedUpdate(UpdateFlags.UpdateText);
        }
    }

    public int maxChars
    {
        get
        {
            this.UpgradeData();
            return this.data.maxChars;
        }
        set
        {
            this.UpgradeData();
            this.data.maxChars = value;
            this.SetNeedUpdate(UpdateFlags.UpdateBuffers);
        }
    }

    public int textureGradient
    {
        get
        {
            this.UpgradeData();
            return this.data.textureGradient;
        }
        set
        {
            this.UpgradeData();
            this.data.textureGradient = value % this.font.gradientCount;
            this.SetNeedUpdate(UpdateFlags.UpdateText);
        }
    }

    public bool inlineStyling
    {
        get
        {
            this.UpgradeData();
            return this.data.inlineStyling;
        }
        set
        {
            this.UpgradeData();
            this.data.inlineStyling = value;
            this.SetNeedUpdate(UpdateFlags.UpdateText);
        }
    }

    public float Spacing
    {
        get
        {
            this.UpgradeData();
            return this.data.spacing;
        }
        set
        {
            this.UpgradeData();
            if (this.data.spacing != value)
            {
                this.data.spacing = value;
                this.SetNeedUpdate(UpdateFlags.UpdateText);
            }
        }
    }

    public float LineSpacing
    {
        get
        {
            this.UpgradeData();
            return this.data.lineSpacing;
        }
        set
        {
            this.UpgradeData();
            if (this.data.lineSpacing != value)
            {
                this.data.lineSpacing = value;
                this.SetNeedUpdate(UpdateFlags.UpdateText);
            }
        }
    }

    public int SortingOrder
    {
        get
        {
            return this.CachedRenderer.sortingOrder;
        }
        set
        {
            if (this.CachedRenderer.sortingOrder != value)
            {
                this.data.renderLayer = value;
                this.CachedRenderer.sortingOrder = value;
            }
        }
    }

    private Renderer CachedRenderer
    {
        get
        {
            if ((UnityEngine.Object)this._cachedRenderer == (UnityEngine.Object)null)
            {
                this._cachedRenderer = base.GetComponent<Renderer>();
            }
            return this._cachedRenderer;
        }
    }

    private bool useInlineStyling
    {
        get
        {
            return this.inlineStyling && this._fontInst.textureGradients;
        }
    }

    private void UpgradeData()
    {
        if (this.data.version != 1)
        {
            this.data.font = this._font;
            this.data.text = this._text;
            this.data.color = this._color;
            this.data.color2 = this._color2;
            this.data.useGradient = this._useGradient;
            this.data.textureGradient = this._textureGradient;
            this.data.anchor = this._anchor;
            this.data.scale = this._scale;
            this.data.kerning = this._kerning;
            this.data.maxChars = this._maxChars;
            this.data.inlineStyling = this._inlineStyling;
            this.data.formatting = this._formatting;
            this.data.wordWrapWidth = this._wordWrapWidth;
            this.data.spacing = this.spacing;
            this.data.lineSpacing = this.lineSpacing;
        }
        this.data.version = 1;
    }

    private static int GetInlineStyleCommandLength(int cmdSymbol)
    {
        int result = 0;
        switch (cmdSymbol)
        {
            case 99:
                result = 5;
                break;
            case 67:
                result = 9;
                break;
            case 103:
                result = 9;
                break;
            case 71:
                result = 17;
                break;
        }
        return result;
    }

    public string FormatText(string unformattedString)
    {
        string empty = string.Empty;
        this.FormatText(ref empty, unformattedString);
        return empty;
    }

    private void FormatText()
    {
        this.FormatText(ref this._formattedText, this.data.text);
    }

    private void FormatText(ref string _targetString, string _source)
    {
        this.InitInstance();
        if (!this.formatting || this.wordWrapWidth == 0 || this._fontInst.texelSize == Vector2.zero)
        {
            _targetString = _source;
        }
        else
        {
            float num = this._fontInst.texelSize.x * (float)this.wordWrapWidth;
            StringBuilder stringBuilder = new StringBuilder(_source.Length);
            float num2 = 0f;
            float num3 = 0f;
            int num4 = -1;
            int num5 = -1;
            bool flag = false;
            for (int i = 0; i < _source.Length; i++)
            {
                char c = _source[i];
                bool flag2 = c == '^';
                tk2dFontChar tk2dFontChar;
                if (this._fontInst.useDictionary)
                {
                    if (!this._fontInst.charDict.ContainsKey(c))
                    {
                        c = '\0';
                    }
                    tk2dFontChar = this._fontInst.charDict[c];
                }
                else
                {
                    if (c >= this._fontInst.chars.Length)
                    {
                        c = '\0';
                    }
                    tk2dFontChar = this._fontInst.chars[c];
                }
                if (flag2)
                {
                    c = '^';
                }
                if (flag)
                {
                    flag = false;
                }
                else
                {
                    if (this.data.inlineStyling && c == '^' && i + 1 < _source.Length)
                    {
                        if (_source[i + 1] != '^')
                        {
                            int inlineStyleCommandLength = tk2dTextMesh.GetInlineStyleCommandLength(_source[i + 1]);
                            int num6 = 1 + inlineStyleCommandLength;
                            for (int j = 0; j < num6; j++)
                            {
                                if (i + j < _source.Length)
                                {
                                    stringBuilder.Append(_source[i + j]);
                                }
                            }
                            i += num6 - 1;
                            continue;
                        }
                        flag = true;
                        stringBuilder.Append('^');
                    }
                    switch (c)
                    {
                        case '\n':
                            num2 = 0f;
                            num3 = 0f;
                            num4 = stringBuilder.Length;
                            num5 = i;
                            goto IL_02e1;
                        case ' ':
                            num2 += (tk2dFontChar.advance + this.data.spacing) * this.data.scale.x;
                            num3 = num2;
                            num4 = stringBuilder.Length;
                            num5 = i;
                            goto IL_02e1;
                        default:
                            {
                                if (num2 + tk2dFontChar.p1.x * this.data.scale.x > num)
                                {
                                    if (num3 > 0f)
                                    {
                                        num3 = 0f;
                                        num2 = 0f;
                                        stringBuilder.Remove(num4 + 1, stringBuilder.Length - num4 - 1);
                                        stringBuilder.Append('\n');
                                        i = num5;
                                        break;
                                    }
                                    stringBuilder.Append('\n');
                                    num2 = (tk2dFontChar.advance + this.data.spacing) * this.data.scale.x;
                                }
                                else
                                {
                                    num2 += (tk2dFontChar.advance + this.data.spacing) * this.data.scale.x;
                                }
                                goto IL_02e1;
                            }
                        IL_02e1:
                            stringBuilder.Append(c);
                            break;
                    }
                }
            }
            _targetString = stringBuilder.ToString();
        }
    }

    private void SetNeedUpdate(UpdateFlags uf)
    {
        if (this.updateFlags == UpdateFlags.UpdateNone)
        {
            this.updateFlags |= uf;
            tk2dUpdateManager.QueueCommit(this);
        }
        else
        {
            this.updateFlags |= uf;
        }
    }

    private void InitInstance()
    {
        if (this.data != null && (UnityEngine.Object)this.data.font != (UnityEngine.Object)null)
        {
            this._fontInst = this.data.font.inst;
            this._fontInst.InitDictionary();
        }
    }

    private void Awake()
    {
        this.UpgradeData();
        if ((UnityEngine.Object)this.data.font != (UnityEngine.Object)null)
        {
            this._fontInst = this.data.font.inst;
        }
        this.updateFlags = UpdateFlags.UpdateBuffers;
        if ((UnityEngine.Object)this.data.font != (UnityEngine.Object)null)
        {
            this.Init();
            this.UpdateMaterial();
        }
        this.updateFlags = UpdateFlags.UpdateNone;
    }

    protected void OnDestroy()
    {
        if ((UnityEngine.Object)this.meshFilter == (UnityEngine.Object)null)
        {
            this.meshFilter = base.GetComponent<MeshFilter>();
        }
        if ((UnityEngine.Object)this.meshFilter != (UnityEngine.Object)null)
        {
            this.mesh = this.meshFilter.sharedMesh;
        }
        if ((bool)this.mesh)
        {
            UnityEngine.Object.DestroyImmediate(this.mesh, true);
            this.meshFilter.mesh = null;
        }
    }

    public int NumDrawnCharacters()
    {
        int num = this.NumTotalCharacters();
        if (num > this.data.maxChars)
        {
            num = this.data.maxChars;
        }
        return num;
    }

    public int NumTotalCharacters()
    {
        this.InitInstance();
        if ((this.updateFlags & (UpdateFlags.UpdateText | UpdateFlags.UpdateBuffers)) != 0)
        {
            this.FormatText();
        }
        int num = 0;
        for (int i = 0; i < this._formattedText.Length; i++)
        {
            int num2 = this._formattedText[i];
            bool flag = num2 == 94;
            if (this._fontInst.useDictionary)
            {
                if (!this._fontInst.charDict.ContainsKey(num2))
                {
                    num2 = 0;
                }
            }
            else if (num2 >= this._fontInst.chars.Length)
            {
                num2 = 0;
            }
            if (flag)
            {
                num2 = 94;
            }
            if (num2 != 10)
            {
                if (this.data.inlineStyling && num2 == 94 && i + 1 < this._formattedText.Length)
                {
                    if (this._formattedText[i + 1] != '^')
                    {
                        i += tk2dTextMesh.GetInlineStyleCommandLength(this._formattedText[i + 1]);
                        continue;
                    }
                    i++;
                }
                num++;
            }
        }
        return num;
    }

    [Obsolete("Use GetEstimatedMeshBoundsForString().size instead")]
    public Vector2 GetMeshDimensionsForString(string str)
    {
        return tk2dTextGeomGen.GetMeshDimensionsForString(str, tk2dTextGeomGen.Data(this.data, this._fontInst, this._formattedText));
    }

    public Bounds GetEstimatedMeshBoundsForString(string str)
    {
        this.InitInstance();
        tk2dTextGeomGen.GeomData geomData = tk2dTextGeomGen.Data(this.data, this._fontInst, this._formattedText);
        Vector2 meshDimensionsForString = tk2dTextGeomGen.GetMeshDimensionsForString(this.FormatText(str), geomData);
        float yAnchorForHeight = tk2dTextGeomGen.GetYAnchorForHeight(meshDimensionsForString.y, geomData);
        float xAnchorForWidth = tk2dTextGeomGen.GetXAnchorForWidth(meshDimensionsForString.x, geomData);
        float num = (this._fontInst.lineHeight + this.data.lineSpacing) * this.data.scale.y;
        return new Bounds(new Vector3(xAnchorForWidth + meshDimensionsForString.x * 0.5f, yAnchorForHeight + meshDimensionsForString.y * 0.5f + num, 0f), Vector3.Scale(meshDimensionsForString, new Vector3(1f, -1f, 1f)));
    }

    public void Init(bool force)
    {
        if (force)
        {
            this.SetNeedUpdate(UpdateFlags.UpdateBuffers);
        }
        this.Init();
    }

    public void Init()
    {
        if ((bool)this._fontInst)
        {
            if ((this.updateFlags & UpdateFlags.UpdateBuffers) == UpdateFlags.UpdateNone && !((UnityEngine.Object)this.mesh == (UnityEngine.Object)null))
            {
                return;
            }
            this._fontInst.InitDictionary();
            this.FormatText();
            tk2dTextGeomGen.GeomData geomData = tk2dTextGeomGen.Data(this.data, this._fontInst, this._formattedText);
            int num = default(int);
            int num2 = default(int);
            tk2dTextGeomGen.GetTextMeshGeomDesc(out num, out num2, geomData);
            this.vertices = new Vector3[num];
            this.uvs = new Vector2[num];
            this.colors = new Color32[num];
            this.untintedColors = new Color32[num];
            if (this._fontInst.textureGradients)
            {
                this.uv2 = new Vector2[num];
            }
            int[] array = new int[num2];
            int target = tk2dTextGeomGen.SetTextMeshGeom(this.vertices, this.uvs, this.uv2, this.untintedColors, 0, geomData);
            if (!this._fontInst.isPacked)
            {
                Color32 color = this.data.color;
                Color32 color2 = (!this.data.useGradient) ? this.data.color : this.data.color2;
                for (int i = 0; i < num; i++)
                {
                    Color32 color3 = (i % 4 >= 2) ? color2 : color;
                    byte b = (byte)(this.untintedColors[i].r * color3.r / 255);
                    byte b2 = (byte)(this.untintedColors[i].g * color3.g / 255);
                    byte b3 = (byte)(this.untintedColors[i].b * color3.b / 255);
                    byte b4 = (byte)(this.untintedColors[i].a * color3.a / 255);
                    if (this._fontInst.premultipliedAlpha)
                    {
                        b = (byte)(b * b4 / 255);
                        b2 = (byte)(b2 * b4 / 255);
                        b3 = (byte)(b3 * b4 / 255);
                    }
                    this.colors[i] = new Color32(b, b2, b3, b4);
                }
            }
            else
            {
                this.colors = this.untintedColors;
            }
            tk2dTextGeomGen.SetTextMeshIndices(array, 0, 0, geomData, target);
            if ((UnityEngine.Object)this.mesh == (UnityEngine.Object)null)
            {
                if ((UnityEngine.Object)this.meshFilter == (UnityEngine.Object)null)
                {
                    this.meshFilter = base.GetComponent<MeshFilter>();
                }
                this.mesh = new Mesh();
                this.mesh.MarkDynamic();
                this.mesh.hideFlags = HideFlags.DontSave;
                this.meshFilter.mesh = this.mesh;
            }
            else
            {
                this.mesh.Clear();
            }
            this.mesh.vertices = this.vertices;
            this.mesh.uv = this.uvs;
            if (this.font.textureGradients)
            {
                this.mesh.uv2 = this.uv2;
            }
            this.mesh.triangles = array;
            this.mesh.colors32 = this.colors;
            this.mesh.RecalculateBounds();
            this.mesh.bounds = tk2dBaseSprite.AdjustedMeshBounds(this.mesh.bounds, this.data.renderLayer);
            this.updateFlags = UpdateFlags.UpdateNone;
        }
    }

    public void Commit()
    {
        tk2dUpdateManager.FlushQueues();
    }

    public void DoNotUse__CommitInternal()
    {
        this.InitInstance();
        if (!((UnityEngine.Object)this._fontInst == (UnityEngine.Object)null))
        {
            this._fontInst.InitDictionary();
            if ((this.updateFlags & UpdateFlags.UpdateBuffers) != 0 || (UnityEngine.Object)this.mesh == (UnityEngine.Object)null)
            {
                this.Init();
            }
            else
            {
                if ((this.updateFlags & UpdateFlags.UpdateText) != 0)
                {
                    this.FormatText();
                    tk2dTextGeomGen.GeomData geomData = tk2dTextGeomGen.Data(this.data, this._fontInst, this._formattedText);
                    int num = tk2dTextGeomGen.SetTextMeshGeom(this.vertices, this.uvs, this.uv2, this.untintedColors, 0, geomData);
                    for (int i = num; i < this.data.maxChars; i++)
                    {
                        this.vertices[i * 4] = (this.vertices[i * 4 + 1] = (this.vertices[i * 4 + 2] = (this.vertices[i * 4 + 3] = Vector3.zero)));
                    }
                    this.mesh.vertices = this.vertices;
                    this.mesh.uv = this.uvs;
                    if (this._fontInst.textureGradients)
                    {
                        this.mesh.uv2 = this.uv2;
                    }
                    if (this._fontInst.isPacked)
                    {
                        this.colors = this.untintedColors;
                        this.mesh.colors32 = this.colors;
                    }
                    if (this.data.inlineStyling)
                    {
                        this.SetNeedUpdate(UpdateFlags.UpdateColors);
                    }
                    this.mesh.RecalculateBounds();
                    this.mesh.bounds = tk2dBaseSprite.AdjustedMeshBounds(this.mesh.bounds, this.data.renderLayer);
                }
                if (!this.font.isPacked && (this.updateFlags & UpdateFlags.UpdateColors) != 0)
                {
                    Color32 color = this.data.color;
                    Color32 color2 = (!this.data.useGradient) ? this.data.color : this.data.color2;
                    for (int j = 0; j < this.colors.Length; j++)
                    {
                        Color32 color3 = (j % 4 >= 2) ? color2 : color;
                        byte b = (byte)(this.untintedColors[j].r * color3.r / 255);
                        byte b2 = (byte)(this.untintedColors[j].g * color3.g / 255);
                        byte b3 = (byte)(this.untintedColors[j].b * color3.b / 255);
                        byte b4 = (byte)(this.untintedColors[j].a * color3.a / 255);
                        if (this._fontInst.premultipliedAlpha)
                        {
                            b = (byte)(b * b4 / 255);
                            b2 = (byte)(b2 * b4 / 255);
                            b3 = (byte)(b3 * b4 / 255);
                        }
                        this.colors[j] = new Color32(b, b2, b3, b4);
                    }
                    this.mesh.colors32 = this.colors;
                }
            }
            this.updateFlags = UpdateFlags.UpdateNone;
        }
    }

    public void MakePixelPerfect()
    {
        float num = 1f;
        tk2dCamera tk2dCamera = tk2dCamera.CameraForLayer(base.gameObject.layer);
        if ((UnityEngine.Object)tk2dCamera != (UnityEngine.Object)null)
        {
            if (this._fontInst.version < 1)
            {
                UnityEngine.Debug.LogError("Need to rebuild font.");
            }
            Vector3 position = base.transform.position;
            float z = position.z;
            Vector3 position2 = tk2dCamera.transform.position;
            float distance = z - position2.z;
            float num2 = this._fontInst.invOrthoSize * this._fontInst.halfTargetHeight;
            num = tk2dCamera.GetSizeAtDistance(distance) * num2;
        }
        else if ((bool)Camera.main)
        {
            if (Camera.main.orthographic)
            {
                num = Camera.main.orthographicSize;
            }
            else
            {
                Vector3 position3 = base.transform.position;
                float z2 = position3.z;
                Vector3 position4 = Camera.main.transform.position;
                float zdist = z2 - position4.z;
                num = tk2dPixelPerfectHelper.CalculateScaleForPerspectiveCamera(Camera.main.fieldOfView, zdist);
            }
            num *= this._fontInst.invOrthoSize;
        }
        Vector3 scale = this.scale;
        float x = Mathf.Sign(scale.x) * num;
        Vector3 scale2 = this.scale;
        float y = Mathf.Sign(scale2.y) * num;
        Vector3 scale3 = this.scale;
        this.scale = new Vector3(x, y, Mathf.Sign(scale3.z) * num);
    }

    public bool UsesSpriteCollection(tk2dSpriteCollectionData spriteCollection)
    {
        if ((UnityEngine.Object)this.data.font != (UnityEngine.Object)null && (UnityEngine.Object)this.data.font.spriteCollection != (UnityEngine.Object)null)
        {
            return (UnityEngine.Object)this.data.font.spriteCollection == (UnityEngine.Object)spriteCollection;
        }
        return true;
    }

    private void UpdateMaterial()
    {
        if ((UnityEngine.Object)base.GetComponent<Renderer>().sharedMaterial != (UnityEngine.Object)this._fontInst.materialInst)
        {
            base.GetComponent<Renderer>().material = this._fontInst.materialInst;
        }
    }

    public void ForceBuild()
    {
        if ((UnityEngine.Object)this.data.font != (UnityEngine.Object)null)
        {
            this._fontInst = this.data.font.inst;
            this.UpdateMaterial();
        }
        this.Init(true);
    }
}


