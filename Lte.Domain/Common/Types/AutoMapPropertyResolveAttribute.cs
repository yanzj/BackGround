using System;

namespace Lte.Domain.Common.Types
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AutoMapPropertyResolveAttribute : Attribute
    {
        public string PeerMemberName { get; }

        public Type TargetType { get; }

        public Type ResolveActionType { get; }

        public AutoMapPropertyResolveAttribute(string peerMemberName, Type targetType, Type resolvActionType = null)
        {
            PeerMemberName = peerMemberName;
            TargetType = targetType;
            ResolveActionType = resolvActionType;
        }
    }
}