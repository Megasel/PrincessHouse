// ILSpyBased#2
using System.Collections.Generic;
using tk2dRuntime;
using UnityEngine;

[AddComponentMenu("2D Toolkit/Sprite/tk2dStaticSpriteBatcher")]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class tk2dStaticSpriteBatcher : MonoBehaviour, ISpriteCollectionForceBuild
{
    public enum Flags
    {
        None,
        GenerateCollider,
        FlattenDepth,
        SortToCamera = 4
    }

    public static int CURRENT_VERSION = 3;

    public int version;

    public tk2dBatchedSprite[] batchedSprites;

    public tk2dTextMeshData[] allTextMeshData;

    public tk2dSpriteCollectionData spriteCollection;

    [SerializeField]
    private Flags flags = Flags.GenerateCollider;

    private Mesh mesh;

    private Mesh colliderMesh;

    [SerializeField]
    private Vector3 _scale = new Vector3(1f, 1f, 1f);

    public bool CheckFlag(Flags mask)
    {
        return (this.flags & mask) != Flags.None;
    }

    public void SetFlag(Flags mask, bool value)
    {
        if (this.CheckFlag(mask) != value)
        {
            if (value)
            {
                this.flags |= mask;
            }
            else
            {
                this.flags &= ~mask;
            }
            this.Build();
        }
    }

    private void Awake()
    {
        this.Build();
    }

    private bool UpgradeData()
    {
        if (this.version == tk2dStaticSpriteBatcher.CURRENT_VERSION)
        {
            return false;
        }
        if (this._scale == Vector3.zero)
        {
            this._scale = Vector3.one;
        }
        if (this.version < 2 && this.batchedSprites != null)
        {
            tk2dBatchedSprite[] array = this.batchedSprites;
            foreach (tk2dBatchedSprite tk2dBatchedSprite in array)
            {
                tk2dBatchedSprite.parentId = -1;
            }
        }
        if (this.version < 3)
        {
            if (this.batchedSprites != null)
            {
                tk2dBatchedSprite[] array2 = this.batchedSprites;
                foreach (tk2dBatchedSprite tk2dBatchedSprite2 in array2)
                {
                    if (tk2dBatchedSprite2.spriteId == -1)
                    {
                        tk2dBatchedSprite2.type = tk2dBatchedSprite.Type.EmptyGameObject;
                    }
                    else
                    {
                        tk2dBatchedSprite2.type = tk2dBatchedSprite.Type.Sprite;
                        if ((Object)tk2dBatchedSprite2.spriteCollection == (Object)null)
                        {
                            tk2dBatchedSprite2.spriteCollection = this.spriteCollection;
                        }
                    }
                }
                this.UpdateMatrices();
            }
            this.spriteCollection = null;
        }
        this.version = tk2dStaticSpriteBatcher.CURRENT_VERSION;
        return true;
    }

    protected void OnDestroy()
    {
        if ((bool)this.mesh)
        {
            UnityEngine.Object.DestroyImmediate(this.mesh);
        }
        if ((bool)this.colliderMesh)
        {
            UnityEngine.Object.DestroyImmediate(this.colliderMesh);
        }
    }

    public void UpdateMatrices()
    {
        bool flag = false;
        tk2dBatchedSprite[] array = this.batchedSprites;
        foreach (tk2dBatchedSprite tk2dBatchedSprite in array)
        {
            if (tk2dBatchedSprite.parentId != -1)
            {
                flag = true;
                break;
            }
        }
        if (flag)
        {
            Matrix4x4 rhs = default(Matrix4x4);
            List<tk2dBatchedSprite> list = new List<tk2dBatchedSprite>(this.batchedSprites);
            list.Sort((tk2dBatchedSprite a, tk2dBatchedSprite b) => a.parentId.CompareTo(b.parentId));
            foreach (tk2dBatchedSprite item in list)
            {
                rhs.SetTRS(item.position, item.rotation, item.localScale);
                item.relativeMatrix = ((item.parentId != -1) ? this.batchedSprites[item.parentId].relativeMatrix : Matrix4x4.identity) * rhs;
            }
        }
        else
        {
            tk2dBatchedSprite[] array2 = this.batchedSprites;
            foreach (tk2dBatchedSprite tk2dBatchedSprite2 in array2)
            {
                tk2dBatchedSprite2.relativeMatrix.SetTRS(tk2dBatchedSprite2.position, tk2dBatchedSprite2.rotation, tk2dBatchedSprite2.localScale);
            }
        }
    }

    public void Build()
    {
        this.UpgradeData();
        if ((Object)this.mesh == (Object)null)
        {
            this.mesh = new Mesh();
            this.mesh.hideFlags = HideFlags.DontSave;
            base.GetComponent<MeshFilter>().mesh = this.mesh;
        }
        else
        {
            this.mesh.Clear();
        }
        if ((bool)this.colliderMesh)
        {
            UnityEngine.Object.DestroyImmediate(this.colliderMesh);
            this.colliderMesh = null;
        }
        if (this.batchedSprites != null && this.batchedSprites.Length != 0)
        {
            this.SortBatchedSprites();
            this.BuildRenderMesh();
            this.BuildPhysicsMesh();
        }
    }

    private void SortBatchedSprites()
    {
        List<tk2dBatchedSprite> list = new List<tk2dBatchedSprite>();
        List<tk2dBatchedSprite> list2 = new List<tk2dBatchedSprite>();
        List<tk2dBatchedSprite> list3 = new List<tk2dBatchedSprite>();
        tk2dBatchedSprite[] array = this.batchedSprites;
        foreach (tk2dBatchedSprite tk2dBatchedSprite in array)
        {
            if (!tk2dBatchedSprite.IsDrawn)
            {
                list3.Add(tk2dBatchedSprite);
            }
            else
            {
                Material material = this.GetMaterial(tk2dBatchedSprite);
                if ((Object)material != (Object)null)
                {
                    if (material.renderQueue == 2000)
                    {
                        list.Add(tk2dBatchedSprite);
                    }
                    else
                    {
                        list2.Add(tk2dBatchedSprite);
                    }
                }
                else
                {
                    list.Add(tk2dBatchedSprite);
                }
            }
        }
        List<tk2dBatchedSprite> list4 = new List<tk2dBatchedSprite>(list.Count + list2.Count + list3.Count);
        list4.AddRange(list);
        list4.AddRange(list2);
        list4.AddRange(list3);
        Dictionary<tk2dBatchedSprite, int> dictionary = new Dictionary<tk2dBatchedSprite, int>();
        int num = 0;
        foreach (tk2dBatchedSprite item in list4)
        {
            dictionary[item] = num++;
        }
        foreach (tk2dBatchedSprite item2 in list4)
        {
            if (item2.parentId != -1)
            {
                item2.parentId = dictionary[this.batchedSprites[item2.parentId]];
            }
        }
        this.batchedSprites = list4.ToArray();
    }

    private Material GetMaterial(tk2dBatchedSprite bs)
    {
        switch (bs.type)
        {
            case tk2dBatchedSprite.Type.EmptyGameObject:
                return null;
            case tk2dBatchedSprite.Type.TextMesh:
                return this.allTextMeshData[bs.xRefId].font.materialInst;
            default:
                return bs.GetSpriteDefinition().materialInst;
        }
    }

    private void BuildRenderMesh()
    {
        List<Material> list = new List<Material>();
        List<List<int>> list2 = new List<List<int>>();
        bool flag = false;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = this.CheckFlag(Flags.FlattenDepth);
        tk2dBatchedSprite[] array = this.batchedSprites;
        foreach (tk2dBatchedSprite tk2dBatchedSprite in array)
        {
            tk2dSpriteDefinition spriteDefinition = tk2dBatchedSprite.GetSpriteDefinition();
            if (spriteDefinition != null)
            {
                flag |= (spriteDefinition.normals != null && spriteDefinition.normals.Length > 0);
                flag2 |= (spriteDefinition.tangents != null && spriteDefinition.tangents.Length > 0);
            }
            if (tk2dBatchedSprite.type == tk2dBatchedSprite.Type.TextMesh)
            {
                tk2dTextMeshData tk2dTextMeshData = this.allTextMeshData[tk2dBatchedSprite.xRefId];
                if ((Object)tk2dTextMeshData.font != (Object)null && tk2dTextMeshData.font.inst.textureGradients)
                {
                    flag3 = true;
                }
            }
        }
        List<int> list3 = new List<int>();
        List<int> list4 = new List<int>();
        int num = 0;
        tk2dBatchedSprite[] array2 = this.batchedSprites;
        foreach (tk2dBatchedSprite tk2dBatchedSprite2 in array2)
        {
            if (!tk2dBatchedSprite2.IsDrawn)
            {
                break;
            }
            tk2dSpriteDefinition spriteDefinition2 = tk2dBatchedSprite2.GetSpriteDefinition();
            int num2 = 0;
            int item = 0;
            switch (tk2dBatchedSprite2.type)
            {
                case tk2dBatchedSprite.Type.Sprite:
                    if (spriteDefinition2 != null)
                    {
                        tk2dSpriteGeomGen.GetSpriteGeomDesc(out num2, out item, spriteDefinition2);
                    }
                    break;
                case tk2dBatchedSprite.Type.TiledSprite:
                    if (spriteDefinition2 != null)
                    {
                        tk2dSpriteGeomGen.GetTiledSpriteGeomDesc(out num2, out item, spriteDefinition2, tk2dBatchedSprite2.Dimensions);
                    }
                    break;
                case tk2dBatchedSprite.Type.SlicedSprite:
                    if (spriteDefinition2 != null)
                    {
                        tk2dSpriteGeomGen.GetSlicedSpriteGeomDesc(out num2, out item, spriteDefinition2, tk2dBatchedSprite2.CheckFlag(tk2dBatchedSprite.Flags.SlicedSprite_BorderOnly));
                    }
                    break;
                case tk2dBatchedSprite.Type.ClippedSprite:
                    if (spriteDefinition2 != null)
                    {
                        tk2dSpriteGeomGen.GetClippedSpriteGeomDesc(out num2, out item, spriteDefinition2);
                    }
                    break;
                case tk2dBatchedSprite.Type.TextMesh:
                    {
                        tk2dTextMeshData tk2dTextMeshData2 = this.allTextMeshData[tk2dBatchedSprite2.xRefId];
                        tk2dTextGeomGen.GetTextMeshGeomDesc(out num2, out item, tk2dTextGeomGen.Data(tk2dTextMeshData2, tk2dTextMeshData2.font.inst, tk2dBatchedSprite2.FormattedText));
                        break;
                    }
            }
            num += num2;
            list3.Add(num2);
            list4.Add(item);
        }
        Vector3[] array3 = (!flag) ? null : new Vector3[num];
        Vector4[] array4 = (!flag2) ? null : new Vector4[num];
        Vector3[] array5 = new Vector3[num];
        Color32[] array6 = new Color32[num];
        Vector2[] array7 = new Vector2[num];
        Vector2[] array8 = (!flag3) ? null : new Vector2[num];
        int num3 = 0;
        Material material = null;
        List<int> list5 = null;
        Matrix4x4 identity = Matrix4x4.identity;
        identity.m00 = this._scale.x;
        identity.m11 = this._scale.y;
        identity.m22 = this._scale.z;
        int num4 = 0;
        tk2dBatchedSprite[] array9 = this.batchedSprites;
        foreach (tk2dBatchedSprite tk2dBatchedSprite3 in array9)
        {
            if (!tk2dBatchedSprite3.IsDrawn)
            {
                break;
            }
            if (tk2dBatchedSprite3.type == tk2dBatchedSprite.Type.EmptyGameObject)
            {
                num4++;
            }
            else
            {
                tk2dSpriteDefinition spriteDefinition3 = tk2dBatchedSprite3.GetSpriteDefinition();
                int num5 = list3[num4];
                int num6 = list4[num4];
                Material material2 = this.GetMaterial(tk2dBatchedSprite3);
                if ((Object)material2 != (Object)material)
                {
                    if ((Object)material != (Object)null)
                    {
                        list.Add(material);
                        list2.Add(list5);
                    }
                    material = material2;
                    list5 = new List<int>();
                }
                Vector3[] array10 = new Vector3[num5];
                Vector2[] array11 = new Vector2[num5];
                Vector2[] array12 = (!flag3) ? null : new Vector2[num5];
                Color32[] array13 = new Color32[num5];
                Vector3[] array14 = (!flag) ? null : new Vector3[num5];
                Vector4[] array15 = (!flag2) ? null : new Vector4[num5];
                int[] array16 = new int[num6];
                Vector3 zero = Vector3.zero;
                Vector3 zero2 = Vector3.zero;
                switch (tk2dBatchedSprite3.type)
                {
                    case tk2dBatchedSprite.Type.Sprite:
                        if (spriteDefinition3 != null)
                        {
                            tk2dSpriteGeomGen.SetSpriteGeom(array10, array11, array14, array15, 0, spriteDefinition3, Vector3.one);
                            tk2dSpriteGeomGen.SetSpriteIndices(array16, 0, num3, spriteDefinition3);
                        }
                        break;
                    case tk2dBatchedSprite.Type.TiledSprite:
                        if (spriteDefinition3 != null)
                        {
                            tk2dSpriteGeomGen.SetTiledSpriteGeom(array10, array11, 0, out zero, out zero2, spriteDefinition3, Vector3.one, tk2dBatchedSprite3.Dimensions, tk2dBatchedSprite3.anchor, tk2dBatchedSprite3.BoxColliderOffsetZ, tk2dBatchedSprite3.BoxColliderExtentZ);
                            tk2dSpriteGeomGen.SetTiledSpriteIndices(array16, 0, num3, spriteDefinition3, tk2dBatchedSprite3.Dimensions);
                        }
                        break;
                    case tk2dBatchedSprite.Type.SlicedSprite:
                        if (spriteDefinition3 != null)
                        {
                            tk2dSpriteGeomGen.SetSlicedSpriteGeom(array10, array11, 0, out zero, out zero2, spriteDefinition3, Vector3.one, tk2dBatchedSprite3.Dimensions, tk2dBatchedSprite3.SlicedSpriteBorderBottomLeft, tk2dBatchedSprite3.SlicedSpriteBorderTopRight, tk2dBatchedSprite3.anchor, tk2dBatchedSprite3.BoxColliderOffsetZ, tk2dBatchedSprite3.BoxColliderExtentZ);
                            tk2dSpriteGeomGen.SetSlicedSpriteIndices(array16, 0, num3, spriteDefinition3, tk2dBatchedSprite3.CheckFlag(tk2dBatchedSprite.Flags.SlicedSprite_BorderOnly));
                        }
                        break;
                    case tk2dBatchedSprite.Type.ClippedSprite:
                        if (spriteDefinition3 != null)
                        {
                            tk2dSpriteGeomGen.SetClippedSpriteGeom(array10, array11, 0, out zero, out zero2, spriteDefinition3, Vector3.one, tk2dBatchedSprite3.ClippedSpriteRegionBottomLeft, tk2dBatchedSprite3.ClippedSpriteRegionTopRight, tk2dBatchedSprite3.BoxColliderOffsetZ, tk2dBatchedSprite3.BoxColliderExtentZ);
                            tk2dSpriteGeomGen.SetClippedSpriteIndices(array16, 0, num3, spriteDefinition3);
                        }
                        break;
                    case tk2dBatchedSprite.Type.TextMesh:
                        {
                            tk2dTextMeshData tk2dTextMeshData3 = this.allTextMeshData[tk2dBatchedSprite3.xRefId];
                            tk2dTextGeomGen.GeomData geomData = tk2dTextGeomGen.Data(tk2dTextMeshData3, tk2dTextMeshData3.font.inst, tk2dBatchedSprite3.FormattedText);
                            int target = tk2dTextGeomGen.SetTextMeshGeom(array10, array11, array12, array13, 0, geomData);
                            if (!geomData.fontInst.isPacked)
                            {
                                Color32 color = tk2dTextMeshData3.color;
                                Color32 color2 = (!tk2dTextMeshData3.useGradient) ? tk2dTextMeshData3.color : tk2dTextMeshData3.color2;
                                for (int l = 0; l < array13.Length; l++)
                                {
                                    Color32 color3 = (l % 4 >= 2) ? color2 : color;
                                    byte b = (byte)(array13[l].r * color3.r / 255);
                                    byte b2 = (byte)(array13[l].g * color3.g / 255);
                                    byte b3 = (byte)(array13[l].b * color3.b / 255);
                                    byte b4 = (byte)(array13[l].a * color3.a / 255);
                                    if (geomData.fontInst.premultipliedAlpha)
                                    {
                                        b = (byte)(b * b4 / 255);
                                        b2 = (byte)(b2 * b4 / 255);
                                        b3 = (byte)(b3 * b4 / 255);
                                    }
                                    array13[l] = new Color32(b, b2, b3, b4);
                                }
                            }
                            tk2dTextGeomGen.SetTextMeshIndices(array16, 0, num3, geomData, target);
                            break;
                        }
                }
                tk2dBatchedSprite3.CachedBoundsCenter = zero;
                tk2dBatchedSprite3.CachedBoundsExtents = zero2;
                if (num5 > 0 && tk2dBatchedSprite3.type != tk2dBatchedSprite.Type.TextMesh)
                {
                    bool premulAlpha = (Object)tk2dBatchedSprite3.spriteCollection != (Object)null && tk2dBatchedSprite3.spriteCollection.premultipliedAlpha;
                    tk2dSpriteGeomGen.SetSpriteColors(array13, 0, num5, tk2dBatchedSprite3.color, premulAlpha);
                }
                Matrix4x4 matrix4x = identity * tk2dBatchedSprite3.relativeMatrix;
                for (int m = 0; m < num5; m++)
                {
                    Vector3 vector = Vector3.Scale(array10[m], tk2dBatchedSprite3.baseScale);
                    vector = matrix4x.MultiplyPoint(vector);
                    if (flag4)
                    {
                        vector.z = 0f;
                    }
                    array5[num3 + m] = vector;
                    array7[num3 + m] = array11[m];
                    if (flag3)
                    {
                        array8[num3 + m] = array12[m];
                    }
                    array6[num3 + m] = array13[m];
                    if (flag)
                    {
                        array3[num3 + m] = tk2dBatchedSprite3.rotation * array14[m];
                    }
                    if (flag2)
                    {
                        Vector3 point = new Vector3(array15[m].x, array15[m].y, array15[m].z);
                        point = tk2dBatchedSprite3.rotation * point;
                        array4[num3 + m] = new Vector4(point.x, point.y, point.z, array15[m].w);
                    }
                }
                list5.AddRange(array16);
                num3 += num5;
                num4++;
            }
        }
        if (list5 != null)
        {
            list.Add(material);
            list2.Add(list5);
        }
        if ((bool)this.mesh)
        {
            this.mesh.vertices = array5;
            this.mesh.uv = array7;
            if (flag3)
            {
                this.mesh.uv2 = array8;
            }
            this.mesh.colors32 = array6;
            if (flag)
            {
                this.mesh.normals = array3;
            }
            if (flag2)
            {
                this.mesh.tangents = array4;
            }
            this.mesh.subMeshCount = list2.Count;
            for (int n = 0; n < list2.Count; n++)
            {
                this.mesh.SetTriangles(list2[n].ToArray(), n);
            }
            this.mesh.RecalculateBounds();
        }
        base.GetComponent<Renderer>().sharedMaterials = list.ToArray();
    }

    private void BuildPhysicsMesh()
    {
        MeshCollider component = base.GetComponent<MeshCollider>();
        if ((Object)component != (Object)null)
        {
            if ((Object)base.GetComponent<Collider>() != (Object)component)
            {
                return;
            }
            if (!this.CheckFlag(Flags.GenerateCollider))
            {
                UnityEngine.Object.DestroyImmediate(component);
            }
        }
        EdgeCollider2D[] components = base.GetComponents<EdgeCollider2D>();
        if (!this.CheckFlag(Flags.GenerateCollider))
        {
            EdgeCollider2D[] array = components;
            foreach (EdgeCollider2D obj in array)
            {
                UnityEngine.Object.DestroyImmediate(obj);
            }
        }
        if (this.CheckFlag(Flags.GenerateCollider))
        {
            bool flattenDepth = this.CheckFlag(Flags.FlattenDepth);
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            bool flag = true;
            tk2dBatchedSprite[] array2 = this.batchedSprites;
            foreach (tk2dBatchedSprite tk2dBatchedSprite in array2)
            {
                if (!tk2dBatchedSprite.IsDrawn)
                {
                    break;
                }
                tk2dSpriteDefinition spriteDefinition = tk2dBatchedSprite.GetSpriteDefinition();
                bool flag2 = false;
                bool flag3 = false;
                switch (tk2dBatchedSprite.type)
                {
                    case tk2dBatchedSprite.Type.Sprite:
                        if (spriteDefinition != null && spriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Mesh)
                        {
                            flag2 = true;
                        }
                        if (spriteDefinition != null && spriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Box)
                        {
                            flag3 = true;
                        }
                        break;
                    case tk2dBatchedSprite.Type.TiledSprite:
                    case tk2dBatchedSprite.Type.SlicedSprite:
                    case tk2dBatchedSprite.Type.ClippedSprite:
                        flag3 = tk2dBatchedSprite.CheckFlag(tk2dBatchedSprite.Flags.Sprite_CreateBoxCollider);
                        break;
                }
                if (flag2)
                {
                    num += spriteDefinition.colliderIndicesFwd.Length;
                    num2 += spriteDefinition.colliderVertices.Length;
                    num3 += spriteDefinition.edgeCollider2D.Length;
                    num3 += spriteDefinition.polygonCollider2D.Length;
                }
                else if (flag3)
                {
                    num += 36;
                    num2 += 8;
                    num3++;
                }
                if (spriteDefinition.physicsEngine == tk2dSpriteDefinition.PhysicsEngine.Physics2D)
                {
                    flag = false;
                }
            }
            if (flag && num == 0)
            {
                goto IL_01d5;
            }
            if (!flag && num3 == 0)
            {
                goto IL_01d5;
            }
            if (flag)
            {
                EdgeCollider2D[] array3 = components;
                foreach (EdgeCollider2D obj2 in array3)
                {
                    UnityEngine.Object.DestroyImmediate(obj2);
                }
            }
            else
            {
                if ((Object)this.colliderMesh != (Object)null)
                {
                    UnityEngine.Object.DestroyImmediate(this.colliderMesh);
                }
                if ((Object)component != (Object)null)
                {
                    UnityEngine.Object.DestroyImmediate(component);
                }
            }
            if (flag)
            {
                this.BuildPhysicsMesh3D(component, flattenDepth, num2, num);
            }
            else
            {
                this.BuildPhysicsMesh2D(components, num3);
            }
        }
        return;
    IL_01d5:
        if ((Object)this.colliderMesh != (Object)null)
        {
            UnityEngine.Object.DestroyImmediate(this.colliderMesh);
            this.colliderMesh = null;
        }
        if ((Object)component != (Object)null)
        {
            UnityEngine.Object.DestroyImmediate(component);
        }
        EdgeCollider2D[] array4 = components;
        foreach (EdgeCollider2D obj3 in array4)
        {
            UnityEngine.Object.DestroyImmediate(obj3);
        }
    }

    private void BuildPhysicsMesh2D(EdgeCollider2D[] edgeColliders, int numEdgeColliders)
    {
        for (int i = numEdgeColliders; i < edgeColliders.Length; i++)
        {
            UnityEngine.Object.DestroyImmediate(edgeColliders[i]);
        }
        Vector2[] array = new Vector2[5];
        if (numEdgeColliders > edgeColliders.Length)
        {
            EdgeCollider2D[] array2 = new EdgeCollider2D[numEdgeColliders];
            int num = Mathf.Min(numEdgeColliders, edgeColliders.Length);
            for (int j = 0; j < num; j++)
            {
                array2[j] = edgeColliders[j];
            }
            for (int k = num; k < numEdgeColliders; k++)
            {
                array2[k] = base.gameObject.AddComponent<EdgeCollider2D>();
            }
            edgeColliders = array2;
        }
        Matrix4x4 identity = Matrix4x4.identity;
        identity.m00 = this._scale.x;
        identity.m11 = this._scale.y;
        identity.m22 = this._scale.z;
        int num2 = 0;
        tk2dBatchedSprite[] array3 = this.batchedSprites;
        foreach (tk2dBatchedSprite tk2dBatchedSprite in array3)
        {
            if (!tk2dBatchedSprite.IsDrawn)
            {
                break;
            }
            tk2dSpriteDefinition spriteDefinition = tk2dBatchedSprite.GetSpriteDefinition();
            bool flag = false;
            bool flag2 = false;
            Vector3 a = Vector3.zero;
            Vector3 b = Vector3.zero;
            switch (tk2dBatchedSprite.type)
            {
                case tk2dBatchedSprite.Type.Sprite:
                    if (spriteDefinition != null && spriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Mesh)
                    {
                        flag = true;
                    }
                    if (spriteDefinition != null && spriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Box)
                    {
                        flag2 = true;
                        a = spriteDefinition.colliderVertices[0];
                        b = spriteDefinition.colliderVertices[1];
                    }
                    break;
                case tk2dBatchedSprite.Type.TiledSprite:
                case tk2dBatchedSprite.Type.SlicedSprite:
                case tk2dBatchedSprite.Type.ClippedSprite:
                    flag2 = tk2dBatchedSprite.CheckFlag(tk2dBatchedSprite.Flags.Sprite_CreateBoxCollider);
                    if (flag2)
                    {
                        a = tk2dBatchedSprite.CachedBoundsCenter;
                        b = tk2dBatchedSprite.CachedBoundsExtents;
                    }
                    break;
            }
            Matrix4x4 matrix4x = identity * tk2dBatchedSprite.relativeMatrix;
            if (flag)
            {
                tk2dCollider2DData[] edgeCollider2D = spriteDefinition.edgeCollider2D;
                foreach (tk2dCollider2DData tk2dCollider2DData in edgeCollider2D)
                {
                    Vector2[] array4 = new Vector2[tk2dCollider2DData.points.Length];
                    for (int n = 0; n < tk2dCollider2DData.points.Length; n++)
                    {
                        array4[n] = matrix4x.MultiplyPoint(tk2dCollider2DData.points[n]);
                    }
                    edgeColliders[num2].points = array4;
                }
                tk2dCollider2DData[] polygonCollider2D = spriteDefinition.polygonCollider2D;
                foreach (tk2dCollider2DData tk2dCollider2DData2 in polygonCollider2D)
                {
                    Vector2[] array5 = new Vector2[tk2dCollider2DData2.points.Length + 1];
                    for (int num4 = 0; num4 < tk2dCollider2DData2.points.Length; num4++)
                    {
                        array5[num4] = matrix4x.MultiplyPoint(tk2dCollider2DData2.points[num4]);
                    }
                    array5[tk2dCollider2DData2.points.Length] = array5[0];
                    edgeColliders[num2].points = array5;
                }
                num2++;
            }
            else if (flag2)
            {
                Vector3 vector = a - b;
                Vector3 vector2 = a + b;
                array[0] = matrix4x.MultiplyPoint(new Vector2(vector.x, vector.y));
                array[1] = matrix4x.MultiplyPoint(new Vector2(vector2.x, vector.y));
                array[2] = matrix4x.MultiplyPoint(new Vector2(vector2.x, vector2.y));
                array[3] = matrix4x.MultiplyPoint(new Vector2(vector.x, vector2.y));
                array[4] = array[0];
                edgeColliders[num2].points = array;
                num2++;
            }
        }
    }

    private void BuildPhysicsMesh3D(MeshCollider meshCollider, bool flattenDepth, int numVertices, int numIndices)
    {
        if ((Object)meshCollider == (Object)null)
        {
            meshCollider = base.gameObject.AddComponent<MeshCollider>();
        }
        if ((Object)this.colliderMesh == (Object)null)
        {
            this.colliderMesh = new Mesh();
            this.colliderMesh.hideFlags = HideFlags.DontSave;
        }
        else
        {
            this.colliderMesh.Clear();
        }
        int num = 0;
        Vector3[] array = new Vector3[numVertices];
        int num2 = 0;
        int[] array2 = new int[numIndices];
        Matrix4x4 identity = Matrix4x4.identity;
        identity.m00 = this._scale.x;
        identity.m11 = this._scale.y;
        identity.m22 = this._scale.z;
        tk2dBatchedSprite[] array3 = this.batchedSprites;
        foreach (tk2dBatchedSprite tk2dBatchedSprite in array3)
        {
            if (!tk2dBatchedSprite.IsDrawn)
            {
                break;
            }
            tk2dSpriteDefinition spriteDefinition = tk2dBatchedSprite.GetSpriteDefinition();
            bool flag = false;
            bool flag2 = false;
            Vector3 origin = Vector3.zero;
            Vector3 extents = Vector3.zero;
            switch (tk2dBatchedSprite.type)
            {
                case tk2dBatchedSprite.Type.Sprite:
                    if (spriteDefinition != null && spriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Mesh)
                    {
                        flag = true;
                    }
                    if (spriteDefinition != null && spriteDefinition.colliderType == tk2dSpriteDefinition.ColliderType.Box)
                    {
                        flag2 = true;
                        origin = spriteDefinition.colliderVertices[0];
                        extents = spriteDefinition.colliderVertices[1];
                    }
                    break;
                case tk2dBatchedSprite.Type.TiledSprite:
                case tk2dBatchedSprite.Type.SlicedSprite:
                case tk2dBatchedSprite.Type.ClippedSprite:
                    flag2 = tk2dBatchedSprite.CheckFlag(tk2dBatchedSprite.Flags.Sprite_CreateBoxCollider);
                    if (flag2)
                    {
                        origin = tk2dBatchedSprite.CachedBoundsCenter;
                        extents = tk2dBatchedSprite.CachedBoundsExtents;
                    }
                    break;
            }
            Matrix4x4 mat = identity * tk2dBatchedSprite.relativeMatrix;
            if (flattenDepth)
            {
                mat.m23 = 0f;
            }
            if (flag)
            {
                tk2dSpriteGeomGen.SetSpriteDefinitionMeshData(array, array2, num, num2, num, spriteDefinition, mat, tk2dBatchedSprite.baseScale);
                num += spriteDefinition.colliderVertices.Length;
                num2 += spriteDefinition.colliderIndicesFwd.Length;
            }
            else if (flag2)
            {
                tk2dSpriteGeomGen.SetBoxMeshData(array, array2, num, num2, num, origin, extents, mat, tk2dBatchedSprite.baseScale);
                num += 8;
                num2 += 36;
            }
        }
        this.colliderMesh.vertices = array;
        this.colliderMesh.triangles = array2;
        meshCollider.sharedMesh = this.colliderMesh;
    }

    public bool UsesSpriteCollection(tk2dSpriteCollectionData spriteCollection)
    {
        return (Object)this.spriteCollection == (Object)spriteCollection;
    }

    public void ForceBuild()
    {
        this.Build();
    }
}


