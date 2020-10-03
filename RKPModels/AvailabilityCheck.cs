using System;

namespace RKPModels
{

    public class AvailabilityCheck
    {
        public static bool IsAvailable(string[] productkey, MaterialMatrix avaiMat)
        {
            if (avaiMat == null) throw new Exception("Availibility Matrix is null (" + avaiMat + ")");
            // only RKP II
            MaterialMatrix matMat = GetMaterials(productkey);
            if (matMat == null) return false;
            MaterialMatrix m3 = avaiMat - matMat;
            return m3.IsNotNegativ();
        }

        public static MaterialMatrix GetMaterials(string[] produktkey)
        {
            // only RKP II
            if (!produktkey[0].StartsWith("D")) return null;
            MaterialMatrix resultMat = new MaterialMatrix();
            string[] tmp_prodkey = (string[])produktkey.Clone();
            bool next_pump_avai = false;
            do
            {
                string old_pos19 = tmp_prodkey[19];

                if (old_pos19 != "" && old_pos19 != "DS1")
                {
                    tmp_prodkey[19] = "DS1";
                    next_pump_avai = true;
                }
                else next_pump_avai = false;

                MaterialMatrix soloMat = GetMaterialsSolo(tmp_prodkey);
                if (soloMat == null || soloMat.CountNonNegativeEntrys() != 3)
                {
                    return null;
                }
                resultMat = resultMat + soloMat;

                tmp_prodkey[19] = old_pos19;

                if (next_pump_avai)
                {
                    // remove repeating part to simulate solo pump
                    // articlenum might be false then but its only for detect of RKP II

                    for (int cnt = 0; (19 + cnt) < tmp_prodkey.Length; cnt++)
                    {
                        tmp_prodkey[11 + cnt] = tmp_prodkey[19 + cnt];
                    }

                    // all following stages of RKP's have XX cover
                    tmp_prodkey[10] = "XX";
                }
            } while (next_pump_avai);
            return resultMat;
        }
        public static MaterialMatrix GetMaterialsSolo(string[] produktkey)
        {
            // only RKP II
            if (!produktkey[0].StartsWith("D")) return null;
            // no AZP's
            if (produktkey[11] != "RKP" && produktkey[11] != "FRP")
                return null;
            // only solo
            if (!string.IsNullOrWhiteSpace(produktkey[19]) && produktkey[19] != "DS1")
                return null;
            int pumpsize = int.Parse(produktkey[12]);
            // only size <= 140
            if (pumpsize > 140)
                return null;

            MaterialMatrix materialMat = new MaterialMatrix();
            int sizeIndex = -1;
            switch (int.Parse(produktkey[12]))
            {
                case 19: sizeIndex = 0; break;
                case 32: sizeIndex = 1; break;
                case 45: sizeIndex = 2; break;
                case 63: sizeIndex = 3; break;
                case 80: sizeIndex = 4; break;
                case 100: sizeIndex = 5; break;
                case 140: sizeIndex = 6; break;
                default: throw new Exception("unknown pump size");
            }
            if (produktkey[11] == "RKP")
            {
                if (produktkey[8] == "R")
                {
                    if (string.IsNullOrWhiteSpace(produktkey[19]))
                    {
                        // double can be used as single anyway
                        materialMat[sizeIndex, 0]++;
                    }
                    else
                    {
                        materialMat[sizeIndex, 1]++;
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(produktkey[19]))
                    {
                        // double can be used as single anyway
                        materialMat[sizeIndex, 5]++;
                    }
                    else
                    {
                        materialMat[sizeIndex, 6]++;
                    }

                }
            }
            if (produktkey[11] == "FRP")
            {
                if (produktkey[8] == "R")
                {
                    if (string.IsNullOrWhiteSpace(produktkey[19]))
                    {
                        // double can be used as single anyway
                        materialMat[sizeIndex, 2]++;
                    }
                    else
                    {
                        materialMat[sizeIndex, 3]++;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(produktkey[19]))  // solo
            {
                if (produktkey[14] == "C")   //HFC
                {
                    switch (produktkey[10])
                    {
                        case "A1": materialMat[sizeIndex, 20]++; break;
                        default: break;
                    }
                }
                else if (produktkey[14] == "M" || produktkey[14] == "D")     // Mineral-Oil
                {
                    switch (produktkey[10])
                    {
                        case "A1": materialMat[sizeIndex, 7]++; break;
                        case "C3": materialMat[sizeIndex, 10]++; break;
                        case "A7": materialMat[sizeIndex, 13]++; break;
                        case "XX": materialMat[sizeIndex, 16]++; break;
                        default: break;
                    }
                }
                else if (produktkey[14] == "A")     // Mineral-Oil
                {
                    switch (produktkey[10])
                    {
                        case "A7": materialMat[sizeIndex, 18]++; break;
                        default: break;
                    }
                }
            }
            else// no-solo (DS1)
            {
                if (produktkey[14] == "C")   //HFC
                {
                    switch (produktkey[10])
                    {
                        case "B1": materialMat[sizeIndex, 19]++; break;
                        default: break;
                    }
                }
                else if (produktkey[14] == "M" || produktkey[14] == "D")     // Mineral-Oil
                {
                    switch (produktkey[10])
                    {
                        case "A1": materialMat[sizeIndex, 8]++; break;
                        case "B1": materialMat[sizeIndex, 9]++; break;
                        case "C3": materialMat[sizeIndex, 11]++; break;
                        case "D3": materialMat[sizeIndex, 12]++; break;
                        case "A7": materialMat[sizeIndex, 14]++; break;
                        case "B7": materialMat[sizeIndex, 15]++; break;
                        case "XX": materialMat[sizeIndex, 17]++; break;
                        default: break;
                    }
                }
            }
            switch (produktkey[16])
            {
                case "F2": materialMat[sizeIndex, 21]++; break;
                case "F1": materialMat[sizeIndex, 22]++; break;
                case "H1": materialMat[sizeIndex, 23]++; break;
                case "H2": materialMat[sizeIndex, 25]++; break;
                case "J1": materialMat[sizeIndex, 26]++; break;
                case "R1": materialMat[sizeIndex, 27]++; break;
                case "U1": materialMat[sizeIndex, 28]++; break;
                case "G2": materialMat[sizeIndex, 29]++; break;
                case "C1": materialMat[sizeIndex, 30]++; break;
                case "E1": materialMat[sizeIndex, 31]++; break;
                case "E2": materialMat[sizeIndex, 32]++; break;
                case "D1": materialMat[sizeIndex, 33]++; break;
                case "D2": materialMat[sizeIndex, 34]++; break;
                case "B2": materialMat[sizeIndex, 37]++; break;
                case "S2": materialMat[sizeIndex, 38]++; break;
                case "S3": materialMat[sizeIndex, 39]++; break;
                case "S1": materialMat[sizeIndex, 40]++; break;
            }
            return materialMat;
        }
        public static bool CanResolve(string[] productkey)
        {
            MaterialMatrix matMat = GetMaterials(productkey);
            return matMat != null;
        }
    }
}


