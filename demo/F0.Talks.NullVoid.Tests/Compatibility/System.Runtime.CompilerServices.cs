using System.ComponentModel;

namespace System.Runtime.CompilerServices;

#if !HAS_ISEXTERNALINIT
[EditorBrowsable(EditorBrowsableState.Never)]
internal static class IsExternalInit
{
}
#endif
