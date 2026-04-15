using System;
using System.Collections.Generic;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    public sealed class MAgentPropertyType : PropertyDbBase
    {
        //Ügynök adatai:
        public Agent MAgent { get; set; }

        //Ingatlan adatai:
        public Property MProperty { get; set; }
        
        //Típusok:
        public PropertyType MPropertyType { get; set; }

        public MAgentPropertyType() { }

        public MAgentPropertyType(MAgentPropertyType mAgentPropertyType)
        {
            this.MAgent = new Agent(mAgentPropertyType.MAgent);
            this.MProperty = new Property(mAgentPropertyType.MProperty);
            this.MPropertyType = new PropertyType(mAgentPropertyType.MPropertyType);
        }

        public void Clone(out PropertyDbBase Clone) => Clone = new MAgentPropertyType(this);

        public void Copy(PropertyDbBase ToCopy)
        {
            var Copy = (MAgentPropertyType)ToCopy;
            this.MProperty.Copy(Copy.MProperty);
            this.MPropertyType.Copy(Copy.MPropertyType);
            this.MAgent.Copy(Copy.MAgent);
        }

        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(this, obj)) return true;
            var other = obj as MAgentPropertyType;
            if(other == null) return false;
            return other.MAgent.Equals(this.MAgent) && other.MProperty.Equals(this.MProperty) && other.MPropertyType.Equals(this.MPropertyType);
        }
    }
}
