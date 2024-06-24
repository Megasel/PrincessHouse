using Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Utilities
{
    public static class ReflectionUtil
    {
        [CompilerGenerated]
        private sealed class _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator0 : IEnumerable, IEnumerable<Type>, IEnumerator, IDisposable, IEnumerator<Type>
        {
            internal Assembly assembly;

            internal Type[] _0024locvar0;

            internal int _0024locvar1;

            internal Type _003Ctype_003E__1;

            internal Type attribute;

            internal Type _0024current;

            internal bool _0024disposing;

            internal int _0024PC;

            Type IEnumerator<Type>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            [DebuggerHidden]
            public _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator0()
            {
            }

            public bool MoveNext()
            {
                uint num = (uint)this._0024PC;
                this._0024PC = -1;
                switch (num)
                {
                    case 0u:
                        this._0024locvar0 = this.assembly.GetTypes();
                        this._0024locvar1 = 0;
                        goto IL_009e;
                    case 1u:
                        this._0024locvar1++;
                        goto IL_009e;
                    default:
                        {
                            return false;
                        }
                    IL_009e:
                        if (this._0024locvar1 < this._0024locvar0.Length)
                        {
                            this._003Ctype_003E__1 = this._0024locvar0[this._0024locvar1];
                            if (this._003Ctype_003E__1.GetCustomAttributes(this.attribute.GetType(), true).Length > 0)
                            {
                                this._0024current = this._003Ctype_003E__1;
                                if (!this._0024disposing)
                                {
                                    this._0024PC = 1;
                                }
                                break;
                            }
                            goto case 1u;
                        }
                        this._0024PC = -1;
                        goto default;
                }
                return true;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this._0024disposing = true;
                this._0024PC = -1;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<Type>)this).GetEnumerator();
            }

            [DebuggerHidden]
            IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
            {
                if (Interlocked.CompareExchange(ref this._0024PC, 0, -2) == -2)
                {
                    return this;
                }
                _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator0 _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator = new _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator0();
                _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator.assembly = this.assembly;
                _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator.attribute = this.attribute;
                return _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator;
            }
        }

        [CompilerGenerated]
        private sealed class _003CGetAllFieldsOrProperties_003Ec__Iterator1<T> : IEnumerable, IEnumerable<T>, IEnumerator, IDisposable, IEnumerator<T>
        {
            internal object obj;

            internal BindingFlags bindingFlags;

            internal T[] _003Cfields_003E__0;

            internal T[] _003Cproperties_003E__0;

            internal int _003Ci_003E__1;

            internal int _003Ci_003E__2;

            internal T _0024current;

            internal bool _0024disposing;

            internal int _0024PC;

            T IEnumerator<T>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            [DebuggerHidden]
            public _003CGetAllFieldsOrProperties_003Ec__Iterator1()
            {
            }

            public bool MoveNext()
            {
                uint num = (uint)this._0024PC;
                this._0024PC = -1;
                switch (num)
                {
                    case 0u:
                        this._003Cfields_003E__0 = Enumerable.ToArray<T>(ReflectionUtil.GetAllFields<T>(this.obj, this.bindingFlags));
                        this._003Cproperties_003E__0 = Enumerable.ToArray<T>(ReflectionUtil.GetAllProperties<T>(this.obj, this.bindingFlags));
                        if (this._003Cfields_003E__0 != null && this._003Cfields_003E__0.Length != 0)
                        {
                            this._003Ci_003E__1 = 0;
                            goto IL_00ba;
                        }
                        goto IL_00cd;
                    case 1u:
                        this._003Ci_003E__1++;
                        goto IL_00ba;
                    case 2u:
                        this._003Ci_003E__2++;
                        goto IL_012a;
                    default:
                        {
                            return false;
                        }
                    IL_00cd:
                        if (this._003Cproperties_003E__0 != null && this._003Cproperties_003E__0.Length != 0)
                        {
                            this._003Ci_003E__2 = 0;
                            goto IL_012a;
                        }
                        goto IL_013d;
                    IL_013d:
                        this._0024PC = -1;
                        goto default;
                    IL_00ba:
                        if (this._003Ci_003E__1 < this._003Cfields_003E__0.Length)
                        {
                            this._0024current = this._003Cfields_003E__0[this._003Ci_003E__1];
                            if (!this._0024disposing)
                            {
                                this._0024PC = 1;
                            }
                            break;
                        }
                        goto IL_00cd;
                    IL_012a:
                        if (this._003Ci_003E__2 < this._003Cproperties_003E__0.Length)
                        {
                            this._0024current = this._003Cproperties_003E__0[this._003Ci_003E__2];
                            if (!this._0024disposing)
                            {
                                this._0024PC = 2;
                            }
                            break;
                        }
                        goto IL_013d;
                }
                return true;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this._0024disposing = true;
                this._0024PC = -1;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<T>)this).GetEnumerator();
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                if (Interlocked.CompareExchange(ref this._0024PC, 0, -2) == -2)
                {
                    return this;
                }
                _003CGetAllFieldsOrProperties_003Ec__Iterator1<T> _003CGetAllFieldsOrProperties_003Ec__Iterator = new _003CGetAllFieldsOrProperties_003Ec__Iterator1<T>();
                _003CGetAllFieldsOrProperties_003Ec__Iterator.obj = this.obj;
                _003CGetAllFieldsOrProperties_003Ec__Iterator.bindingFlags = this.bindingFlags;
                return _003CGetAllFieldsOrProperties_003Ec__Iterator;
            }
        }

        [CompilerGenerated]
        private sealed class _003CGetAllFieldsOrProperties_003Ec__Iterator2 : IEnumerable, IEnumerable<object>, IEnumerator, IDisposable, IEnumerator<object>
        {
            internal object obj;

            internal BindingFlags bindingFlags;

            internal object[] _003Cfields_003E__0;

            internal object[] _003Cproperties_003E__0;

            internal int _003Ci_003E__1;

            internal int _003Ci_003E__2;

            internal object _0024current;

            internal bool _0024disposing;

            internal int _0024PC;

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            [DebuggerHidden]
            public _003CGetAllFieldsOrProperties_003Ec__Iterator2()
            {
            }

            public bool MoveNext()
            {
                uint num = (uint)this._0024PC;
                this._0024PC = -1;
                switch (num)
                {
                    case 0u:
                        this._003Cfields_003E__0 = this.obj.GetAllFields(this.bindingFlags).Cast<object>().ToArray();
                        this._003Cproperties_003E__0 = this.obj.GetAllProperties(this.bindingFlags).Cast<object>().ToArray();
                        if (this._003Cfields_003E__0 != null && this._003Cfields_003E__0.Length != 0)
                        {
                            this._003Ci_003E__1 = 0;
                            goto IL_00c0;
                        }
                        goto IL_00d3;
                    case 1u:
                        this._003Ci_003E__1++;
                        goto IL_00c0;
                    case 2u:
                        this._003Ci_003E__2++;
                        goto IL_012c;
                    default:
                        {
                            return false;
                        }
                    IL_00d3:
                        if (this._003Cproperties_003E__0 != null && this._003Cproperties_003E__0.Length != 0)
                        {
                            this._003Ci_003E__2 = 0;
                            goto IL_012c;
                        }
                        goto IL_013f;
                    IL_013f:
                        this._0024PC = -1;
                        goto default;
                    IL_00c0:
                        if (this._003Ci_003E__1 < this._003Cfields_003E__0.Length)
                        {
                            this._0024current = this._003Cfields_003E__0[this._003Ci_003E__1];
                            if (!this._0024disposing)
                            {
                                this._0024PC = 1;
                            }
                            break;
                        }
                        goto IL_00d3;
                    IL_012c:
                        if (this._003Ci_003E__2 < this._003Cproperties_003E__0.Length)
                        {
                            this._0024current = this._003Cproperties_003E__0[this._003Ci_003E__2];
                            if (!this._0024disposing)
                            {
                                this._0024PC = 2;
                            }
                            break;
                        }
                        goto IL_013f;
                }
                return true;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this._0024disposing = true;
                this._0024PC = -1;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<object>)this).GetEnumerator();
            }

            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                if (Interlocked.CompareExchange(ref this._0024PC, 0, -2) == -2)
                {
                    return this;
                }
                _003CGetAllFieldsOrProperties_003Ec__Iterator2 _003CGetAllFieldsOrProperties_003Ec__Iterator = new _003CGetAllFieldsOrProperties_003Ec__Iterator2();
                _003CGetAllFieldsOrProperties_003Ec__Iterator.obj = this.obj;
                _003CGetAllFieldsOrProperties_003Ec__Iterator.bindingFlags = this.bindingFlags;
                return _003CGetAllFieldsOrProperties_003Ec__Iterator;
            }
        }

        [CompilerGenerated]
        private sealed class _003CGetAllFields_003Ec__Iterator3<T> : IEnumerable, IEnumerable<T>, IEnumerator, IDisposable, IEnumerator<T>
        {
            internal object obj;

            internal BindingFlags bindingFlags;

            internal FieldInfo[] _003Cfields_003E__0;

            internal int _003Ci_003E__1;

            internal object _003CcurrentValue_003E__2;

            internal T _0024current;

            internal bool _0024disposing;

            internal int _0024PC;

            T IEnumerator<T>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            [DebuggerHidden]
            public _003CGetAllFields_003Ec__Iterator3()
            {
            }

            public bool MoveNext()
            {
                uint num = (uint)this._0024PC;
                this._0024PC = -1;
                switch (num)
                {
                    case 0u:
                        this._003Cfields_003E__0 = this.obj.GetType().GetFields(this.bindingFlags);
                        if (this._003Cfields_003E__0 != null && this._003Cfields_003E__0.Length != 0)
                        {
                            this._003Ci_003E__1 = 0;
                            goto IL_00d1;
                        }
                        goto default;
                    case 1u:
                        this._003Ci_003E__1++;
                        goto IL_00d1;
                    default:
                        {
                            return false;
                        }
                    IL_00d1:
                        if (this._003Ci_003E__1 < this._003Cfields_003E__0.Length)
                        {
                            this._003CcurrentValue_003E__2 = this._003Cfields_003E__0[this._003Ci_003E__1].GetValue(this.obj);
                            if (this._003CcurrentValue_003E__2.GetType() == typeof(T))
                            {
                                this._0024current = (T)this._003CcurrentValue_003E__2;
                                if (!this._0024disposing)
                                {
                                    this._0024PC = 1;
                                }
                                break;
                            }
                            goto case 1u;
                        }
                        this._0024PC = -1;
                        goto default;
                }
                return true;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this._0024disposing = true;
                this._0024PC = -1;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<T>)this).GetEnumerator();
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                if (Interlocked.CompareExchange(ref this._0024PC, 0, -2) == -2)
                {
                    return this;
                }
                _003CGetAllFields_003Ec__Iterator3<T> _003CGetAllFields_003Ec__Iterator = new _003CGetAllFields_003Ec__Iterator3<T>();
                _003CGetAllFields_003Ec__Iterator.obj = this.obj;
                _003CGetAllFields_003Ec__Iterator.bindingFlags = this.bindingFlags;
                return _003CGetAllFields_003Ec__Iterator;
            }
        }

        [CompilerGenerated]
        private sealed class _003CGetAllFields_003Ec__Iterator4 : IEnumerable, IEnumerable<object>, IEnumerator, IDisposable, IEnumerator<object>
        {
            internal object obj;

            internal BindingFlags bindingFlags;

            internal FieldInfo[] _003Cfields_003E__0;

            internal int _003Ci_003E__1;

            internal object _0024current;

            internal bool _0024disposing;

            internal int _0024PC;

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            [DebuggerHidden]
            public _003CGetAllFields_003Ec__Iterator4()
            {
            }

            public bool MoveNext()
            {
                uint num = (uint)this._0024PC;
                this._0024PC = -1;
                switch (num)
                {
                    case 0u:
                        this._003Cfields_003E__0 = this.obj.GetType().GetFields(this.bindingFlags);
                        if (this._003Cfields_003E__0 != null && this._003Cfields_003E__0.Length != 0)
                        {
                            this._003Ci_003E__1 = 0;
                            goto IL_00a6;
                        }
                        goto default;
                    case 1u:
                        this._003Ci_003E__1++;
                        goto IL_00a6;
                    default:
                        {
                            return false;
                        }
                    IL_00a6:
                        if (this._003Ci_003E__1 < this._003Cfields_003E__0.Length)
                        {
                            this._0024current = this._003Cfields_003E__0[this._003Ci_003E__1].GetValue(this.obj);
                            if (!this._0024disposing)
                            {
                                this._0024PC = 1;
                            }
                            break;
                        }
                        this._0024PC = -1;
                        goto default;
                }
                return true;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this._0024disposing = true;
                this._0024PC = -1;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<object>)this).GetEnumerator();
            }

            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                if (Interlocked.CompareExchange(ref this._0024PC, 0, -2) == -2)
                {
                    return this;
                }
                _003CGetAllFields_003Ec__Iterator4 _003CGetAllFields_003Ec__Iterator = new _003CGetAllFields_003Ec__Iterator4();
                _003CGetAllFields_003Ec__Iterator.obj = this.obj;
                _003CGetAllFields_003Ec__Iterator.bindingFlags = this.bindingFlags;
                return _003CGetAllFields_003Ec__Iterator;
            }
        }

        [CompilerGenerated]
        private sealed class _003CGetAllProperties_003Ec__Iterator5<T> : IEnumerable, IEnumerable<T>, IEnumerator, IDisposable, IEnumerator<T>
        {
            internal object obj;

            internal BindingFlags bindingFlags;

            internal PropertyInfo[] _003Cproperties_003E__0;

            internal int _003Ci_003E__1;

            internal object _003CcurrentValue_003E__2;

            internal T _0024current;

            internal bool _0024disposing;

            internal int _0024PC;

            T IEnumerator<T>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            [DebuggerHidden]
            public _003CGetAllProperties_003Ec__Iterator5()
            {
            }

            public bool MoveNext()
            {
                uint num = (uint)this._0024PC;
                this._0024PC = -1;
                switch (num)
                {
                    case 0u:
                        this._003Cproperties_003E__0 = this.obj.GetType().GetProperties(this.bindingFlags);
                        if (this._003Cproperties_003E__0 != null && this._003Cproperties_003E__0.Length != 0)
                        {
                            this._003Ci_003E__1 = 0;
                            goto IL_00d2;
                        }
                        goto default;
                    case 1u:
                        this._003Ci_003E__1++;
                        goto IL_00d2;
                    default:
                        {
                            return false;
                        }
                    IL_00d2:
                        if (this._003Ci_003E__1 < this._003Cproperties_003E__0.Length)
                        {
                            this._003CcurrentValue_003E__2 = this._003Cproperties_003E__0[this._003Ci_003E__1].GetValue(this.obj, null);
                            if (this._003CcurrentValue_003E__2.GetType() == typeof(T))
                            {
                                this._0024current = (T)this._003CcurrentValue_003E__2;
                                if (!this._0024disposing)
                                {
                                    this._0024PC = 1;
                                }
                                break;
                            }
                            goto case 1u;
                        }
                        this._0024PC = -1;
                        goto default;
                }
                return true;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this._0024disposing = true;
                this._0024PC = -1;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<T>)this).GetEnumerator();
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                if (Interlocked.CompareExchange(ref this._0024PC, 0, -2) == -2)
                {
                    return this;
                }
                _003CGetAllProperties_003Ec__Iterator5<T> _003CGetAllProperties_003Ec__Iterator = new _003CGetAllProperties_003Ec__Iterator5<T>();
                _003CGetAllProperties_003Ec__Iterator.obj = this.obj;
                _003CGetAllProperties_003Ec__Iterator.bindingFlags = this.bindingFlags;
                return _003CGetAllProperties_003Ec__Iterator;
            }
        }

        [CompilerGenerated]
        private sealed class _003CGetAllProperties_003Ec__Iterator6 : IEnumerable, IEnumerable<object>, IEnumerator, IDisposable, IEnumerator<object>
        {
            internal object obj;

            internal BindingFlags bindingFlags;

            internal PropertyInfo[] _003Cproperties_003E__0;

            internal int _003Ci_003E__1;

            internal object _0024current;

            internal bool _0024disposing;

            internal int _0024PC;

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._0024current;
                }
            }

            [DebuggerHidden]
            public _003CGetAllProperties_003Ec__Iterator6()
            {
            }

            public bool MoveNext()
            {
                uint num = (uint)this._0024PC;
                this._0024PC = -1;
                switch (num)
                {
                    case 0u:
                        this._003Cproperties_003E__0 = this.obj.GetType().GetProperties(this.bindingFlags);
                        if (this._003Cproperties_003E__0 != null && this._003Cproperties_003E__0.Length != 0)
                        {
                            this._003Ci_003E__1 = 0;
                            goto IL_00a7;
                        }
                        goto default;
                    case 1u:
                        this._003Ci_003E__1++;
                        goto IL_00a7;
                    default:
                        {
                            return false;
                        }
                    IL_00a7:
                        if (this._003Ci_003E__1 < this._003Cproperties_003E__0.Length)
                        {
                            this._0024current = this._003Cproperties_003E__0[this._003Ci_003E__1].GetValue(this.obj, null);
                            if (!this._0024disposing)
                            {
                                this._0024PC = 1;
                            }
                            break;
                        }
                        this._0024PC = -1;
                        goto default;
                }
                return true;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this._0024disposing = true;
                this._0024PC = -1;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<object>)this).GetEnumerator();
            }

            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator()
            {
                if (Interlocked.CompareExchange(ref this._0024PC, 0, -2) == -2)
                {
                    return this;
                }
                _003CGetAllProperties_003Ec__Iterator6 _003CGetAllProperties_003Ec__Iterator = new _003CGetAllProperties_003Ec__Iterator6();
                _003CGetAllProperties_003Ec__Iterator.obj = this.obj;
                _003CGetAllProperties_003Ec__Iterator.bindingFlags = this.bindingFlags;
                return _003CGetAllProperties_003Ec__Iterator;
            }
        }

        private static Assembly[] _allAssemblies;

        public static Assembly[] AllAssemblies
        {
            get
            {
                if (ReflectionUtil._allAssemblies == null)
                {
                    ReflectionUtil._allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                }
                return ReflectionUtil._allAssemblies;
            }
        }

        [DebuggerHidden]
        public static IEnumerable<Type> GetAllTypesWithAttributeAsEnumerable(this Assembly assembly, Type attribute)
        {
            _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator0 _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator = new _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator0();
            _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator.assembly = assembly;
            _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator.attribute = attribute;
            _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator0 _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator2 = _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator;
            _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator2._0024PC = -2;
            return _003CGetAllTypesWithAttributeAsEnumerable_003Ec__Iterator2;
        }

        public static Type[] GetAllTypesWithAttribute(this Assembly assembly, Type attribute)
        {
            return assembly.GetAllTypesWithAttributeAsEnumerable(attribute).ToArray();
        }

        public static T GetNestedObject<T>(this object obj, string path)
        {
            string[] array = path.Split('.');
            foreach (string name in array)
            {
                obj = obj.GetFieldOrProperty<T>(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            }
            return (T)obj;
        }

        public static T GetFieldOrProperty<T>(this object obj, string name, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            try
            {
                return obj.GetField<T>(name, bindingFlags);
            }
            catch (FieldNotFoundException)
            {
                try
                {
                    return obj.GetProperty<T>(name, bindingFlags);
                }
                catch (PropertyNotFoundException)
                {
                    throw new PropertyOrFieldNotFoundException("Couldn't find a filed nor a property with the name of '" + name + "' inside of the object '" + obj.GetType().Name + "'");
                }
            }
        }

        public static T GetField<T>(this object obj, string name, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            FieldInfo field = obj.GetType().GetField(name, bindingFlags);
            if (field != null)
            {
                return (T)field.GetValue(obj);
            }
            throw new FieldNotFoundException("Couldn't find a field with the name of '" + name + "' inside of the object '" + obj.GetType().Name + "'");
        }

        public static T GetProperty<T>(this object obj, string name, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            PropertyInfo property = obj.GetType().GetProperty(name, bindingFlags);
            if (property != null)
            {
                return (T)property.GetValue(obj, null);
            }
            throw new PropertyNotFoundException("Couldn't find a property with the name of '" + name + "' inside of the object '" + obj.GetType().Name + "'");
        }

        public static void SetFieldOrProperty<T>(this object obj, string name, T value, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            try
            {
                obj.SetField(name, value, bindingFlags);
            }
            catch (FieldNotFoundException)
            {
                try
                {
                    obj.SetProperty(name, value, bindingFlags);
                }
                catch (PropertyNotFoundException)
                {
                    throw new PropertyOrFieldNotFoundException("Couldn't find a filed nor a property with the name of '" + name + "' inside of the object '" + obj.GetType().Name + "'");
                }
            }
        }

        public static void SetField<T>(this object obj, string name, T value, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            FieldInfo field = obj.GetType().GetField(name, bindingFlags);
            if (field != null)
            {
                field.SetValue(obj, value);
                return;
            }
            throw new FieldNotFoundException("Couldn't find a field with the name of '" + name + "' inside of the object '" + obj.GetType().Name + "'");
        }

        public static void SetProperty<T>(this object obj, string name, T value, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            PropertyInfo property = obj.GetType().GetProperty(name, bindingFlags);
            if (property != null)
            {
                property.SetValue(obj, value, null);
                return;
            }
            throw new PropertyNotFoundException("Couldn't find a property with the name of '" + name + "' inside of the object '" + obj.GetType().Name + "'");
        }

        [DebuggerHidden]
        public static IEnumerable<T> GetAllFieldsOrProperties<T>(this object obj, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            _003CGetAllFieldsOrProperties_003Ec__Iterator1<T> _003CGetAllFieldsOrProperties_003Ec__Iterator = new _003CGetAllFieldsOrProperties_003Ec__Iterator1<T>();
            _003CGetAllFieldsOrProperties_003Ec__Iterator.obj = obj;
            _003CGetAllFieldsOrProperties_003Ec__Iterator.bindingFlags = bindingFlags;
            _003CGetAllFieldsOrProperties_003Ec__Iterator1<T> _003CGetAllFieldsOrProperties_003Ec__Iterator2 = _003CGetAllFieldsOrProperties_003Ec__Iterator;
            _003CGetAllFieldsOrProperties_003Ec__Iterator2._0024PC = -2;
            return _003CGetAllFieldsOrProperties_003Ec__Iterator2;
        }

        [DebuggerHidden]
        public static IEnumerable GetAllFieldsOrProperties(this object obj, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            _003CGetAllFieldsOrProperties_003Ec__Iterator2 _003CGetAllFieldsOrProperties_003Ec__Iterator = new _003CGetAllFieldsOrProperties_003Ec__Iterator2();
            _003CGetAllFieldsOrProperties_003Ec__Iterator.obj = obj;
            _003CGetAllFieldsOrProperties_003Ec__Iterator.bindingFlags = bindingFlags;
            _003CGetAllFieldsOrProperties_003Ec__Iterator2 _003CGetAllFieldsOrProperties_003Ec__Iterator2 = _003CGetAllFieldsOrProperties_003Ec__Iterator;
            _003CGetAllFieldsOrProperties_003Ec__Iterator2._0024PC = -2;
            return _003CGetAllFieldsOrProperties_003Ec__Iterator2;
        }

        [DebuggerHidden]
        public static IEnumerable<T> GetAllFields<T>(this object obj, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            _003CGetAllFields_003Ec__Iterator3<T> _003CGetAllFields_003Ec__Iterator = new _003CGetAllFields_003Ec__Iterator3<T>();
            _003CGetAllFields_003Ec__Iterator.obj = obj;
            _003CGetAllFields_003Ec__Iterator.bindingFlags = bindingFlags;
            _003CGetAllFields_003Ec__Iterator3<T> _003CGetAllFields_003Ec__Iterator2 = _003CGetAllFields_003Ec__Iterator;
            _003CGetAllFields_003Ec__Iterator2._0024PC = -2;
            return _003CGetAllFields_003Ec__Iterator2;
        }

        [DebuggerHidden]
        public static IEnumerable GetAllFields(this object obj, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            _003CGetAllFields_003Ec__Iterator4 _003CGetAllFields_003Ec__Iterator = new _003CGetAllFields_003Ec__Iterator4();
            _003CGetAllFields_003Ec__Iterator.obj = obj;
            _003CGetAllFields_003Ec__Iterator.bindingFlags = bindingFlags;
            _003CGetAllFields_003Ec__Iterator4 _003CGetAllFields_003Ec__Iterator2 = _003CGetAllFields_003Ec__Iterator;
            _003CGetAllFields_003Ec__Iterator2._0024PC = -2;
            return _003CGetAllFields_003Ec__Iterator2;
        }

        [DebuggerHidden]
        public static IEnumerable<T> GetAllProperties<T>(this object obj, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            _003CGetAllProperties_003Ec__Iterator5<T> _003CGetAllProperties_003Ec__Iterator = new _003CGetAllProperties_003Ec__Iterator5<T>();
            _003CGetAllProperties_003Ec__Iterator.obj = obj;
            _003CGetAllProperties_003Ec__Iterator.bindingFlags = bindingFlags;
            _003CGetAllProperties_003Ec__Iterator5<T> _003CGetAllProperties_003Ec__Iterator2 = _003CGetAllProperties_003Ec__Iterator;
            _003CGetAllProperties_003Ec__Iterator2._0024PC = -2;
            return _003CGetAllProperties_003Ec__Iterator2;
        }

        [DebuggerHidden]
        public static IEnumerable GetAllProperties(this object obj, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        {
            _003CGetAllProperties_003Ec__Iterator6 _003CGetAllProperties_003Ec__Iterator = new _003CGetAllProperties_003Ec__Iterator6();
            _003CGetAllProperties_003Ec__Iterator.obj = obj;
            _003CGetAllProperties_003Ec__Iterator.bindingFlags = bindingFlags;
            _003CGetAllProperties_003Ec__Iterator6 _003CGetAllProperties_003Ec__Iterator2 = _003CGetAllProperties_003Ec__Iterator;
            _003CGetAllProperties_003Ec__Iterator2._0024PC = -2;
            return _003CGetAllProperties_003Ec__Iterator2;
        }
    }
}