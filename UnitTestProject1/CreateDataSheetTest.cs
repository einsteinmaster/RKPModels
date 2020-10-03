using System;
using System.IO;
using ExcelSearchBox;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class CreateDataSheetTest
    {
        const string kennliste_path = @"..\..\..\Kennliste2.xlsx";
        [TestMethod]
        public void NoException()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];            
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[12] = "032";
            obj[19] = "";
            dataSheetCreator.CreateDataSheet(obj);
        }
        [TestMethod]
        public void Descriptions()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "test_descriptions";
            obj[1] = "test_descriptions";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "RKP";
            obj[12] = "032";
            obj[13] = "H";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";
            string content = dataSheetCreator.CreateDataSheet(obj);


            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;test_descriptions
Pumpenschlüssel:;test_descriptions
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;032;32 cm3/U
Pumpenanschlüsse;H;Saug- und Druckanschluss gleich, SAE 1# 6000 psi (ISO 6162-2), Hochdruckreihe bis 350 bar,mit metrischem Gewinde M12, 21mm tief.
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;D1;RKP-D mit Eigendruckversorgung
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;00;Ausgangssignal für Druck und Fördermenge 4…20mA").Replace('#','"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }


            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void DoublePump()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "test_double";
            obj[1] = "test_double";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "RKP";
            obj[12] = "032";
            obj[13] = "H";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "RKP";
            obj[20] = "032";
            obj[21] = "K";
            obj[22] = "M";
            obj[23] = "28";
            obj[24] = "E2";
            obj[25] = "Z";
            obj[26] = "00";
            obj[27] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);



            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;test_double
Pumpenschlüssel:;test_double
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;032;32 cm3/U
Pumpenanschlüsse;H;Saug- und Druckanschluss gleich, SAE 1# 6000 psi (ISO 6162-2), Hochdruckreihe bis 350 bar,mit metrischem Gewinde M12, 21mm tief.
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;D1;RKP-D mit Eigendruckversorgung
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;00;Ausgangssignal für Druck und Fördermenge 4…20mA
Pumpenstufe 2
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;032;32 cm3/U
Pumpenanschlüsse;K;RKP II: Sauganschluß SAE 1 1/2# 3000 psi (ISO 6162-1),mit metrischen Gewinden M12, 24mm tief., Druckanschluß SAE 1# 3000 psi (ISO 6162-1),mit metrischen Gewinden M10, 16mm tief.
Betriebsflüssigkeit;M;Mineralöl, Getriebeöl, biologisch abbaubares Öl
max. Betriebsdruck;28;280 bar für Mineralöl-, HFD-, Polyol- und Isozyanat-Pumpen
Steuerung/Regler;E2;Elektrohydraulische Verstellung EHV 2 (Moog Ventil),mit Fremddruckversorgung (Nur als Ersatz für T2)
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;00;keine").Replace('#','"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }


            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void Position_12_Digital()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position12_Digital";
            obj[1] = "Position12_Digital";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "RKP";
            obj[12] = "032";
            obj[13] = "H";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "A1";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position12_Digital
Pumpenschlüssel:;Position12_Digital
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;032;32 cm3/U
Pumpenanschlüsse;H;Saug- und Druckanschluss gleich, SAE 1# 6000 psi (ISO 6162-2), Hochdruckreihe bis 350 bar,mit metrischem Gewinde M12, 21mm tief.
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;D1;RKP-D mit Eigendruckversorgung
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;A1;Ausgangssignal für Druck und Fördermenge 2…10V mit Ethercat").Replace('#','"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void Position_12_Power1()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position12_Power1";
            obj[1] = "Position12_Power1";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "RKP";
            obj[12] = "032";
            obj[13] = "H";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "S1";
            obj[17] = "Z";
            obj[18] = "05";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position12_Power1
Pumpenschlüssel:;Position12_Power1
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;032;32 cm3/U
Pumpenanschlüsse;H;Saug- und Druckanschluss gleich, SAE 1# 6000 psi (ISO 6162-2), Hochdruckreihe bis 350 bar,mit metrischem Gewinde M12, 21mm tief.
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;S1;Leistungsregler  (System Kraftvergleich)
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;05;5,5 kW   (RKP 32, 45)").Replace('#', '"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void Position_12_Power2()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position12_Power2";
            obj[1] = "Position12_Power2";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "RKP";
            obj[12] = "032";
            obj[13] = "H";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "ZK";
            obj[17] = "Z";
            obj[18] = "05";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position12_Power2
Pumpenschlüssel:;Position12_Power2
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;032;32 cm3/U
Pumpenanschlüsse;H;Saug- und Druckanschluss gleich, SAE 1# 6000 psi (ISO 6162-2), Hochdruckreihe bis 350 bar,mit metrischem Gewinde M12, 21mm tief.
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;ZK;Leistungsregler (System Wegvergleich)
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;05;5,5 kW   (RKP 32, 45)").Replace('#','"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void Position_07_19()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position7_19";
            obj[1] = "Position7_19";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "RKP";
            obj[12] = "019";
            obj[13] = "H";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "ZK";
            obj[17] = "Z";
            obj[18] = "05";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position7_19
Pumpenschlüssel:;Position7_19
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;019;19 cm3/U
Pumpenanschlüsse;H;Saug- und Druckanschluss gleich, SAE 3/4# 6000 psi (ISO 6162-2) Hochdruckreihe bis 350 bar,mit metrischen Gewinden M10, 16mm tief.
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;ZK;Leistungsregler (System Wegvergleich)
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;05;5,5 kW   (RKP 32, 45)").Replace('#','"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void Position_07_32()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position7_32";
            obj[1] = "Position7_32";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "RKP";
            obj[12] = "032";
            obj[13] = "K";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position7_32
Pumpenschlüssel:;Position7_32
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;032;32 cm3/U
Pumpenanschlüsse;K;RKP II: Sauganschluß SAE 1 1/2# 3000 psi (ISO 6162-1),mit metrischen Gewinden M12, 24mm tief., Druckanschluß SAE 1# 3000 psi (ISO 6162-1),mit metrischen Gewinden M10, 16mm tief.
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;D1;RKP-D mit Eigendruckversorgung
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;00;Ausgangssignal für Druck und Fördermenge 4…20mA").Replace('#', '"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void Position_07_63()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position7_63";
            obj[1] = "Position7_63";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "RKP";
            obj[12] = "063";
            obj[13] = "H";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position7_63
Pumpenschlüssel:;Position7_63
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;063;63 cm3/U
Pumpenanschlüsse;H;Saug- und Druckanschluss gleich, SAE 1 1/4# 6000 psi (ISO 6162-2) Hochdruckreihe bis 350 bar,mit metrischen Gewinden M14, 24mm tief.
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;D1;RKP-D mit Eigendruckversorgung
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;00;Ausgangssignal für Druck und Fördermenge 4…20mA").Replace('#', '"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void Position_07_80()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position7_80";
            obj[1] = "Position7_80";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "RKP";
            obj[12] = "080";
            obj[13] = "T";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position7_80
Pumpenschlüssel:;Position7_80
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;080;80 cm3/U
Pumpenanschlüsse;T;RKP II: Sauganschluß SAE 2# 3000 psi (ISO 6162-1),mit metrischen Gewinden M12, 22mm tief., Druckanschluß SAE 1 1/4# 6000 psi (ISO 6162-1),mit metrischen Gewinden M14, 24mm tief.
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;D1;RKP-D mit Eigendruckversorgung
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;00;Ausgangssignal für Druck und Fördermenge 4…20mA").Replace('#', '"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void Position_07_100()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position7_100";
            obj[1] = "Position7_100";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "RKP";
            obj[12] = "100";
            obj[13] = "L";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position7_100
Pumpenschlüssel:;Position7_100
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;100;100 cm3/U
Pumpenanschlüsse;L;Sauganschluß SAE 1 1/2# 3000 psi (ISO 6162-1),mit metrischen Gewinden M12, 24mm tief., Druckanschluß SAE 1 1/4# 6000 psi (ISO 6162-1),mit metrischen Gewinden M14, 24mm tief.
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;D1;RKP-D mit Eigendruckversorgung
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;00;Ausgangssignal für Druck und Fördermenge 4…20mA").Replace('#', '"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void Position_07_140()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position7_140";
            obj[1] = "Position7_140";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "RKP";
            obj[12] = "140";
            obj[13] = "T";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position7_140
Pumpenschlüssel:;Position7_140
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;140;140 cm3/U
Pumpenanschlüsse;T;RKP II: Sauganschluß SAE 2 1/2# 3000 psi (ISO 6162-1),mit metrischen Gewinden M12, 22mm tief., Druckanschluß SAE 1 1/2# 6000 psi (ISO 6162-1),mit metrischen Gewinden M16, 25mm tief.
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;D1;RKP-D mit Eigendruckversorgung
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;00;Ausgangssignal für Druck und Fördermenge 4…20mA").Replace('#', '"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }


        [TestMethod]
        public void Position_06_AZP_A()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position6_AZP_A";
            obj[1] = "Position6_AZP_A";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "AZP";
            obj[12] = "031";
            obj[13] = "R";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position6_AZP_A
Pumpenschlüssel:;Position6_AZP_A
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;AZP;Moog Außenzahnradpumpe ,mit Flansch SAE-A und SAE-B
Fördervolumen;031;31 cm³/U   SAE-A
Pumpenanschlüsse;R;Deutscher 4-Loch-Flansch SAE-A,Sauganschluss M6: Lochkreisdurchmesser 40mm,Druckanschluss M6: Lochkreisdurchmesser 35mm
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;D1;RKP-D mit Eigendruckversorgung
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;00;Ausgangssignal für Druck und Fördermenge 4…20mA").Replace('#', '"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void Position_06_AZP_B()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position6_AZP_B";
            obj[1] = "Position6_AZP_B";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "AZP";
            obj[12] = "033";
            obj[13] = "R";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position6_AZP_B
Pumpenschlüssel:;Position6_AZP_B
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;AZP;Moog Außenzahnradpumpe ,mit Flansch SAE-A und SAE-B
Fördervolumen;033;33 cm³/U   SAE-B
Pumpenanschlüsse;R;Deutscher 4-Loch-Flansch SAE-B,Sauganschluss M8: Lochkreisdurchmesser 55mm,Druckanschluss M8: Lochkreisdurchmesser 55mm
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;D1;RKP-D mit Eigendruckversorgung
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;00;Ausgangssignal für Druck und Fördermenge 4…20mA").Replace('#', '"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void Position_06_AZP_Wrong_Connector()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position_6_AZP_Wrong_Connector";
            obj[1] = "Position_6_AZP_Wrong_Connector";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "AZP";
            obj[12] = "033";
            obj[13] = "H";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "D1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position_6_AZP_Wrong_Connector
Pumpenschlüssel:;Position_6_AZP_Wrong_Connector
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;AZP;Moog Außenzahnradpumpe ,mit Flansch SAE-A und SAE-B
Fördervolumen;033;33 cm³/U   SAE-B
Pumpenanschlüsse;H;
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;D1;RKP-D mit Eigendruckversorgung
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;00;Ausgangssignal für Druck und Fördermenge 4…20mA").Replace('#', '"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }

        [TestMethod]
        public void Position_12_NoInfo()
        {
            DataSheetCreator dataSheetCreator = new DataSheetCreator(kennliste_path);
            string[] obj = new string[100];
            for (int cnt = 0; cnt < obj.Length; cnt++) obj[cnt] = "testStr_" + cnt;
            obj[0] = "Position_12_NoInfo";
            obj[1] = "Position_12_NoInfo";
            obj[7] = "HK";
            obj[8] = "L";
            obj[9] = "14";
            obj[10] = "B3";
            obj[11] = "RKP";
            obj[12] = "032";
            obj[13] = "H";
            obj[14] = "C";
            obj[15] = "35";
            obj[16] = "A1";
            obj[17] = "Z";
            obj[18] = "00";
            obj[19] = "";

            string content = dataSheetCreator.CreateDataSheet(obj);

            var str1 = content.Trim();
            var str2 = (@"Pumpennummer:;Position_12_NoInfo
Pumpenschlüssel:;Position_12_NoInfo
Typ;HK;Explosionsgeschützte Pumpe Gas und Staub
Drehrichtung;L;Auf Antrieb gesehen „links”
Drehzahl;14;n = 1400 min-1
Welle und Antriebsflansch;B3;unsinnige Kombination aus metrischer Welle B und SAE-Flansch 3
Pumpenart ;RKP;Radialkolbenpumpe verstellbar, offene Kreis
Fördervolumen;032;32 cm3/U
Pumpenanschlüsse;H;Saug- und Druckanschluss gleich, SAE 1# 6000 psi (ISO 6162-2), Hochdruckreihe bis 350 bar,mit metrischem Gewinde M12, 21mm tief.
Betriebsflüssigkeit;C;HFC (Wasserglycol)
max. Betriebsdruck;35;350 bar für Mineralöl- , HFD- und Skydrol-Pumpen
Steuerung/Regler;A1;Handrad für Hubeinstellung
Zusatzeinrichtung;Z;ohne Zusatzeinrichtung
Zusatzangabe;00;keine").Replace('#', '"').Trim();


            if (str1.Length != str2.Length)
            {
                Console.WriteLine("Length diff");
            }
            string line1 = "";
            string line2 = "";
            for (int cnt = 0; cnt < str1.Length && cnt < str2.Length; cnt++)
            {
                if (str2[cnt] == '\n' && str1[cnt] == '\n')
                {
                    line1 = "";
                    line2 = "";
                }
                else
                {
                    line1 += str1[cnt];
                    line2 += str2[cnt];
                }
                Assert.AreEqual(str1[cnt], str2[cnt], "diffrence at pos " + cnt + " <expected line:\"" + line2 + "\"> <actual line:\"" + line1 + "\">");
            }

            Assert.AreEqual(content.Trim(), str2);
        }



    }
}
