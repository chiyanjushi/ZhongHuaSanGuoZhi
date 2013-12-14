﻿namespace GameObjects.Influences
{
    using GameObjects;
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework;
    using System.Collections;
    using System.Collections.Generic;

    public class Influence : GameObject
    {
        private string description;
        public InfluenceKind Kind;
        private string parameter;
        private string parameter2;

        internal HashSet<ApplyingArchitecture> appliedArch = new HashSet<ApplyingArchitecture>();
        internal HashSet<ApplyingPerson> appliedPerson = new HashSet<ApplyingPerson>();
        internal HashSet<ApplyingFaction> appliedFaction = new HashSet<ApplyingFaction>();
        internal HashSet<ApplyingTroop> appliedTroop = new HashSet<ApplyingTroop>();

        public void ApplyInfluence(Architecture architecture, Applier applier, int applierID)
        {
            ApplyingArchitecture a = new ApplyingArchitecture(architecture, applier, applierID);
            if (appliedArch.Contains(a)) return;
            appliedArch.Add(a);
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            try
            {
                this.Kind.ApplyInfluenceKind(architecture, this, applier, applierID);
            }
            catch
            {
            }
        }

        public void ApplyInfluence(Faction faction, Applier applier, int applierID)
        {
            ApplyingFaction a = new ApplyingFaction(faction, applier, applierID);
            if (appliedFaction.Contains(a)) return;
            appliedFaction.Add(a);
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            try
            {
                this.Kind.ApplyInfluenceKind(faction, this, applier, applierID);
            }
            catch
            {
            }
        }

        public void ApplyInfluence(Person person, Applier applier, int applierID, bool excludePersonal)
        {
            ApplyingPerson a = new ApplyingPerson(person, applier, applierID);
            if (appliedPerson.Contains(a) && 
                (person.LocationTroop == null || appliedTroop.Contains(new ApplyingTroop(person.LocationTroop, applier, applierID)) 
                    || this.Type != InfluenceType.战斗)) return;
            appliedPerson.Add(a);
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            try
            {
                this.Kind.ApplyInfluenceKind(person, this, applier, applierID, excludePersonal);
            }
            catch
            {
            }
        }

        public void ApplyInfluence(Troop troop, Applier applier, int applierID)
        {
            ApplyingTroop a = new ApplyingTroop(troop, applier, applierID);
            if (appliedTroop.Contains(a) && (this.ID < 390 || this.ID > 399)) return;
            appliedTroop.Add(a);
            troop.InfluencesApplying.Add(this);
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            try
            {
                this.Kind.ApplyInfluenceKind(troop, this, applier, applierID);
            }
            catch
            {
            }
        }

        public void DoWork(Architecture architecture)
        {
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            this.Kind.DoWork(architecture);
        }

        public int GetCredit(Troop source, Troop destination)
        {
            return this.Kind.GetCredit(source, destination);
        }

        public int GetCreditWithPosition(Troop source, out Point? position)
        {
            //position = 0;
            position = new Point(0, 0);
            return this.Kind.GetCreditWithPosition(source, out position);
        }

        public bool IsVaild(Person person)
        {
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            return this.Kind.IsVaild(person);
        }

        public bool IsVaild(Troop troop)
        {
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            return this.Kind.IsVaild(troop);
        }

        public void PurifyInfluence(Architecture architecture, Applier applier, int applierID)
        {
            ApplyingArchitecture a = new ApplyingArchitecture(architecture, applier, applierID);
            if (!appliedArch.Contains(a)) return;
            appliedArch.Remove(a);
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            try
            {
                this.Kind.PurifyInfluenceKind(architecture, this, applier, applierID);
            }
            catch
            {
            }
        }

        public void PurifyInfluence(Faction faction, Applier applier, int applierID)
        {
            ApplyingFaction a = new ApplyingFaction(faction, applier, applierID);
            if (!appliedFaction.Contains(a)) return;
            appliedFaction.Remove(a);
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            try
            {
                this.Kind.PurifyInfluenceKind(faction, this, applier, applierID);
            }
            catch
            {
            }
        }

        public void PurifyInfluence(Person person, Applier applier, int applierID, bool excludePersonal)
        {
            ApplyingPerson a = new ApplyingPerson(person, applier, applierID);
            if (!appliedPerson.Contains(a)) return;
            appliedPerson.Remove(a);
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            try
            {
                this.Kind.PurifyInfluenceKind(person, this, applier, applierID, excludePersonal);
            }
            catch
            {
            }
        }

        public void TroopDestroyed(Troop troop)
        {
            appliedTroop.RemoveWhere((x) => { return x.troop == troop; });
        }

        public void PurifyInfluence(Troop troop, Applier applier, int applierID)
        {
            ApplyingTroop a = new ApplyingTroop(troop, applier, applierID);
            if (!appliedTroop.Contains(a)) return;
            appliedTroop.Remove(a);
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            try
            {
                this.Kind.PurifyInfluenceKind(troop, this, applier, applierID);
            }
            catch
            {
            }
        }

        public double AIFacilityValue(Architecture a)
        {
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            try
            {
                return this.Kind.AIFacilityValue(a);
            }
            catch
            {
            }
            return 0;
        }

        public override string ToString()
        {
            return this.Description;
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public string Parameter
        {
            get
            {
                return this.parameter;
            }
            set
            {
                this.parameter = value;
            }
        }

        public string Parameter2
        {
            get
            {
                return this.parameter2;
            }
            set
            {
                this.parameter2 = value;
            }
        }

        public bool TroopLeaderValid
        {
            get
            {
                return this.Kind.TroopLeaderValid;
            }
        }

        public InfluenceType Type
        {
            get
            {
                return this.Kind.Type;
            }
        }

        public double AIPersonValue
        {
            get
            {
                float p1;

                try
                {
                    p1 = float.Parse(this.Parameter);
                }
                catch
                {
                    return this.Kind.AIPersonValue;
                }

                double v;
                switch (this.Kind.ID)
                {
                    case 320:
                        return this.Kind.AIPersonValue *
                            (base.Scenario.GameCommonData.AllCombatMethods.GetCombatMethod((int)p1).Combativity * this.Kind.AIPersonValuePow);
                    case 860:
                        return this.Kind.AIPersonValue *
                            (base.Scenario.GameCommonData.AllStratagems.GetStratagem((int)p1).Combativity * this.Kind.AIPersonValuePow);
                    case 200:
                    case 220:
                        try
                        {
                            int p2 = int.Parse(this.Parameter2);
                            v = this.Kind.AIPersonValue * Math.Pow(p2, this.Kind.AIPersonValuePow);
                            switch ((int) p1)
                            {
                                case 1:
                                case 2:
                                    return v * 2;
                                case 3:
                                case 5:
                                    return v;
                                case 6:
                                    return v * 1.5;
                                case 8:
                                case 9:
                                case 10:
                                    return v * 0.5;
                                default:
                                    return 0;
                            }
                        }
                        catch
                        {
                            return this.Kind.AIPersonValue * Math.Pow(p1, this.Kind.AIPersonValuePow);
                        }
                    case 352:
                        v = this.Kind.AIPersonValue * Math.Pow(p1, this.Kind.AIPersonValuePow);
                        try
                        {
                            float p2 = float.Parse(Parameter2);
                            return this.Kind.AIPersonValue * Math.Min(p1, p2 - 0.5) * Math.Pow(p1, this.Kind.AIPersonValuePow);
                        }
                        catch
                        {
                            return v;
                        }
                    case 6140:
                        v = this.Kind.AIPersonValue * Math.Pow(p1, this.Kind.AIPersonValuePow);
                        try
                        {
                            int p2 = int.Parse(this.Parameter2);
                            if (p1 >= 100)
                            {
                                v *= 1.2;
                            }
                            if (p1 > 110)
                            {
                                v *= 1.5;
                            }
                            return v * p2;
                        }
                        catch
                        {
                            return v;
                        }
                    case 6350:
                        v = this.Kind.AIPersonValue * Math.Pow(p1, this.Kind.AIPersonValuePow);
                        try
                        {
                            int p2 = int.Parse(this.Parameter2);
                            return this.Kind.AIPersonValue * Math.Pow(p2 - 1, p1 / 100.0) * Math.Pow(p1, this.Kind.AIPersonValuePow);
                        }
                        catch
                        {
                            return v;
                        }
                    case 6360:
                        v = this.Kind.AIPersonValue * Math.Pow(p1, this.Kind.AIPersonValuePow);
                        try
                        {
                            int p2 = int.Parse(this.Parameter2);
                            return this.Kind.AIPersonValue * (Math.Max(p2, 100) / 100.0) * Math.Pow(p1, this.Kind.AIPersonValuePow);
                        }
                        catch
                        {
                            return v;
                        }
                    case 6420:
                    case 6430:
                    case 6450:
                        try
                        {
                            float p2 = float.Parse(this.Parameter2);
                            return this.Kind.AIPersonValue * Math.Pow(p2, this.Kind.AIPersonValuePow);
                        }
                        catch
                        {
                            return this.Kind.AIPersonValue * Math.Pow(p1, this.Kind.AIPersonValuePow);
                        }
                    case 6700:
                    case 6705:
                    case 6710:
                    case 6715:
                    case 6720:
                    case 6725:
                    case 6730:
                    case 6735:
                    case 6740:
                    case 6745:
                    case 6760:
                        v = this.Kind.AIPersonValue * Math.Pow(p1, this.Kind.AIPersonValuePow);
                        try
                        {
                            int p2 = int.Parse(this.Parameter2);
                            return v * Math.Pow(p2, 1.2);
                        }
                        catch
                        {
                            return v;
                        }
                    case 6750:
                    case 6755:
                        v = this.Kind.AIPersonValue * Math.Pow(p1, this.Kind.AIPersonValuePow);
                        try
                        {
                            int p2 = int.Parse(this.Parameter2);
                            return v * Math.Pow(p2 / 1000, 1.2);
                        }
                        catch
                        {
                            return v;
                        }
                    default:
                        if (p1 == 0 && this.Kind.AIPersonValuePow <= 0)
                        {
                            v = this.Kind.AIPersonValuePow >= 0 ? this.Kind.AIPersonValue : this.Kind.AIPersonValue * 10;
                        }
                        else
                        {
                            v = this.Kind.AIPersonValue * Math.Pow(p1, this.Kind.AIPersonValuePow);
                        }
                        return v;
                }
            }
        }
    }
}

