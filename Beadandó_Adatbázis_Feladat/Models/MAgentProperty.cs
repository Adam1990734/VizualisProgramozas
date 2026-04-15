using System;
using System.Collections.Generic;
using System.Text;

namespace Beadandó_Adatbázis_Feladat.Models
{
    public sealed class MAgentProperty : PropertyDbBase
    {
        //Ingatlan adatai:
        public Property MProperty { get; set; }
        //Típus adatai:
        public Agent MAgent { get; set; }

        public MAgentProperty() { }
        public MAgentProperty(MAgentProperty mAgentProperty) {
            MAgent = new Agent(mAgentProperty.MAgent);
            MProperty = new Property(mAgentProperty.MProperty);
        }
        public void Clone(out PropertyDbBase Clone) => Clone = new MAgentProperty(this);

        public void Copy(PropertyDbBase ToCopy)
        {
            var Copy = (MAgentProperty)ToCopy;
            this.MAgent.Copy(Copy.MAgent);
            this.MProperty.Copy(Copy.MProperty);
        }

        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(this, obj)) return true;
            var other = obj as MAgentProperty;
            if(other == null) return false;
            return other.MAgent.Equals(this.MAgent) && other.MProperty.Equals(this.MProperty);
        }
    }
}
