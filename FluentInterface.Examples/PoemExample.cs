// ReSharper disable CheckNamespace
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedVariable

using System.Dynamic;

namespace FluentInterface.Examples
{
    public class MyClass
    {
        public class Words : DynamicObject
        {
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = this;
                return true;
            }
        }

        public static void Main()
        {
            dynamic The = new Words();
            var poem =
                The.sun.@is.coming.up.over.the.hill.
                    Or.maybe.its.not.I.cant.even.tell.
                    But.theres.a.warmth.on.my.face.
                    That.isnt.the.blood.
                    And.my.tears.are.turning.
                    The.snow.into.mud.
                    And.I.cant.feel.my.left.leg.
                    But.I.think.its.still.there.
                    Did.I.kill.anybody.
                    Hell.I.never.fight.fair.
                    What.state.am.I.@in.
                    Am.I.still.on.the.run.
                    Has.it.really.been.so.@long.
                    Since.Ive.seen.the.sun;
        }
    }
}

/* (C) Gediminas Geigalas 2013 */



