using System;
using System.Collections.Generic;

namespace RKPModels
{
    public class Statistics
    {
        public List<string[]> allPumps;
        public int countAll;
        public List<string[]> allAvaiPumps;
        public int countAllAvai;
        public List<string[]> RKPPumps;
        public int countRKP;
        public List<string[]> RKPAvaiPumps;
        public int countRKPAvai;
        public List<string[]> resolvablePumps;
        public int resolvable;
        public List<string[]> resolvableAvaiPumps;
        public int resolvableAvai;

        public const string STR_LOADING_STATISTIC_WAIT = "Keine Statistik";
        public const string STR_LOADING_STATISTIC_PROG = "Statistik lädt ....";

        public Statistics()
        {
            allPumps = new List<string[]>();
            allAvaiPumps = new List<string[]>();
            RKPPumps = new List<string[]>();
            RKPAvaiPumps = new List<string[]>();
            resolvablePumps = new List<string[]>();
            resolvableAvaiPumps = new List<string[]>();
        }

        public static Statistics CalcStatistics(ExcelWrapper excelWrapper, MaterialMatrix materialMat)
        {
            Statistics ret = new Statistics();
#if !DEBUG
            try
#endif
            {
                if (materialMat == null || !materialMat.IsNotNegativ() || materialMat.CountNonNegativeEntrys() < 3)
                    return null;
                MaterialMatrix tmpMaterialMatrix = materialMat.Clone();

                for (int colInd = 49; colInd < excelWrapper.GetRowCount(); colInd++)
                {
                    string[] obj = excelWrapper.GetRow(colInd);
                    bool isAvai = false;
                    bool isResolvable = false;
                    if (string.IsNullOrWhiteSpace(obj[0])) throw new Exception();
#if !DEBUG
                    try
#endif
                    {
                        isAvai = AvailabilityCheck.IsAvailable(obj, tmpMaterialMatrix);
                        isResolvable = AvailabilityCheck.CanResolve(obj);
                    }
#if !DEBUG
                    catch (Exception) { }
#endif
                    if (isAvai)
                    {
                        ret.countAllAvai++;
                        ret.allAvaiPumps.Add(obj);
                    }
                    ret.countAll++;
                    ret.allPumps.Add(obj);
                    if (obj[11] == "RKP" || obj[11] == "FRP")
                    {
                        ret.countRKP++;
                        ret.RKPPumps.Add(obj);
                        if (isAvai)
                        {
                            ret.countRKPAvai++;
                            ret.RKPAvaiPumps.Add(obj);
                        }
                    }
                    if (isResolvable)
                    {
                        ret.resolvable++;
                        ret.resolvablePumps.Add(obj);
                        if (isAvai)
                        {
                            ret.resolvableAvai++;
                            ret.resolvableAvaiPumps.Add(obj);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(obj[0])) throw new Exception();
                }

            }
#if !DEBUG
            catch (Exception) { return ret; }
#endif
            return ret;
        }

        public override string ToString()
        {
            return "Abdeckung insgesamt: " + ((100 * countAllAvai) / countAll) + "%\n"
                    + "Abdeckung RKP/FRP: " + ((100 * countRKPAvai) / countRKP) + "%\n"
                + "Abdeckung berücksichtigte RKP/FRP: " + ((100 * resolvableAvai) / resolvable) + "%\n"
                + "Es gibt " + resolvable + " von " + countAll + " berücksichtigte RKP/FRP";
        }
    }
}
