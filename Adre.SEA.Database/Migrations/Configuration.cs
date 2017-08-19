
namespace Adre.SEA.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ASEAContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ASEAContext context)
        {
            SeedEvents(context);
            SeedPhases(context);
            SeedContingent(context);
            SeedAthletes(context);
            ClearData(context);
            SeedEventAthlete(context);

            base.Seed(context);
        }
        
        void SeedEvents(ASEAContext context)
        {
            context.Events.AddOrUpdate(
                new Event { Id = Guid.Parse("B75A0E3F-4204-489E-80C0-6B6595339CAB"), Gender = "M", Name = "Men", Code = "MS", WslId=0 },
                new Event { Id = Guid.Parse("04AB08E3-2C95-46AD-9865-CD5FC9307A66"), Gender = "F", Name = "Women", Code = "WS", WslId = 0 }
            );
            context.SaveChanges();
        }

        private void SeedContingent(ASEAContext context)
        {
            context.Contingents.AddOrUpdate(
                new Contingent { Id = new Guid("E27C77C3-BC18-446C-8934-C772A1FE3765"), Name = "Malaysia", Code = "MAS" },
                new Contingent { Id = new Guid("C93D23CA-6032-4BC4-A09A-AE6DA1FAEB04"), Name = "Brunei", Code = "BRU" },
                new Contingent { Id = new Guid("1F8294A6-D871-4FCE-8409-F097F7665083"), Name = "Cambodia", Code = "CAM" },
                new Contingent { Id = new Guid("745FA236-AB5C-46B1-93F7-45A14A8C7EC0"), Name = "Indonesia", Code = "INA" },
                new Contingent { Id = new Guid("95B8D0A2-B8E2-4EDE-A17C-130D72B01AFC"), Name = "Laos", Code = "LAO" },
                new Contingent { Id = new Guid("2C668FCB-708B-4D40-AE75-381325460813"), Name = "Myammar", Code = "MYA" },
                new Contingent { Id = new Guid("B669165C-D000-4F17-9F3E-2680B7B5C407"), Name = "Philippines", Code = "PHI" },
                new Contingent { Id = new Guid("791CB4C1-4FD6-4FAB-8063-BBB85BAA4261"), Name = "Singapore", Code = "SGP" },
                new Contingent { Id = new Guid("8A707FFD-DDC3-4ADC-AC2E-46CB4842BDF4"), Name = "Thailand", Code = "THA" },
                new Contingent { Id = new Guid("5A56A6E9-6276-443B-B4C4-922C3402E257"), Name = "Timor-Leste", Code = "TLS" },
                new Contingent { Id = new Guid("14056A63-3A16-4910-93D8-494922849D47"), Name = "Vietnam", Code = "VIE" }
            );

            context.SaveChanges();
        }

        void SeedPhases(ASEAContext context)
        {
            context.Phases.AddOrUpdate(
                new Phase { Id = new Guid("41669197-C0F9-4137-AEE2-B13D03622C91"), Name = "Round 1", Order = 0},
                new Phase { Id = new Guid("DFDC45D0-B379-4671-833A-DD87A38557DC"), Name = "Round 2", Order = 1},
                new Phase { Id = new Guid("B32C1F89-1507-46C0-8A83-403C8D3BF806"), Name = "Quater Final", Order = 2},
                new Phase { Id = new Guid("A37EF34A-6305-4778-9CFA-1E452F290949"), Name = "Semi Final", Order = 3},
                new Phase { Id = new Guid("36588A0B-29EA-4294-BD69-72B2EFFE45DB"), Name = "Final", Order = 4}
            );

            context.SaveChanges();
        }

        private void SeedAthletes(ASEAContext context)
        {
            context.Athletes.AddOrUpdate(
                new Athlete { Id = new Guid("36588A0B-29EA-4294-BD69-72B2EFFE45DB"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Myammar"), FullName = "MAZLINA BINTI MUHAMMAD SHAFRI", PreferredName = "MAZLINA MD. SHAFRI" },
                new Athlete { Id = new Guid("1492EA62-5100-45D5-91A3-3E71714F0739"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Myammar"), FullName = "KONG WAN YI", PreferredName = "KONG WAN YI" },
                new Athlete { Id = new Guid("A7896569-7706-4445-9A22-9A04CB72E19E"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Myammar"), FullName = "NURUL IMAN AMANI BINTI MOHD RUSLI", PreferredName = "NURUL IMAN AMANI M.R", },
                new Athlete { Id = new Guid("EF4BEE11-619A-4AC3-AE19-64F1A3502C2D"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Myammar"), FullName = "MAZLINA BINTI MUHAMMAD SHAFRI", PreferredName = "MAZLINA MD. SHAFRI"},
                new Athlete { Id = new Guid("88A57B79-6FBD-4D30-BE11-11AE678B7084"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Myammar"), FullName = "KONG WAN YI", PreferredName = "KONG WAN YI"},
                new Athlete { Id = new Guid("ED15CEDF-C9FB-44D5-8097-A46774374FC2"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Myammar"), FullName = "MOHD RUSLI", PreferredName = "NURUL IMAN AMANI M.R"},
                new Athlete { Id = new Guid("EC1F46E5-C9D8-4B4D-9933-5F695549BC8B"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Timor-Leste"), FullName = "FONG WAN QIN", PreferredName = "FONG WAN QIN"},
                new Athlete { Id = new Guid("70B032A8-1470-457C-A944-7BF8F4E2171A"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Timor-Leste"), FullName = "TAI USHYAN", PreferredName = "TAI USHYAN"},
                new Athlete { Id = new Guid("82B42B46-2033-40FF-86A3-B3AA562BFD06"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Timor-Leste"), FullName = "LEE XINYAO", PreferredName = "LEE XINYAO"},
                new Athlete { Id = new Guid("585B6C71-6A43-4026-A5CD-E8882E642E0E"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Timor-Leste"), FullName = "JESS LIM YEN XIN", PreferredName = "JESS LIM YEN XIN"},
                new Athlete { Id = new Guid("9B46320B-42BE-4510-A6D4-9B52B57E17F9"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Timor-Leste"), FullName = "LEE XINYAO", PreferredName = "LEE XINYAO"},
                new Athlete { Id = new Guid("B11FD93D-CB95-4F8C-9A20-64067A51A7A9"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Timor-Leste"), FullName = "JESS LIM YEN XIN", PreferredName = "JESS LIM YEN XIN"},
                new Athlete { Id = new Guid("26FC48D6-70C5-4A8A-8A19-9FAA3F10FFEE"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Indonesia"), FullName = "FHFJH", PreferredName = "FJHFJH"},
                new Athlete { Id = new Guid("FBDF5D5C-78DC-443C-A4B9-319352925CA8"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Indonesia"), FullName = "RAYNA HOH KHAI LING", PreferredName = "RAYNA HOH KHAI LING"},
                new Athlete { Id = new Guid("D48A504C-2690-4633-82C1-CFEAEA1A56BB"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Indonesia"), FullName = "KHOR EE", PreferredName = "KHOR EE"},
                new Athlete { Id = new Guid("D061686D-86B9-4EC9-A6D4-38DDBE7CD592"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Indonesia"), FullName = "NUR NAIEME BINTI ZAINUL ARIFFIN", PreferredName = "NUR NAIEME ZAINUL A." },
                new Athlete { Id = new Guid("557A156A-CBDE-4076-A738-4FD7754D3A43"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Indonesia"), FullName = "TAMMY NG JIA WEN", PreferredName = "TAMMY NG JIA WEN"},
                new Athlete { Id = new Guid("C899826B-E63C-4FC4-B1A9-C4CCA0E4F926"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Indonesia"), FullName = "NUR NAIEME BINTI ZAINUL ARIFFIN", PreferredName = "NUR NAIEME ZAINUL A."},
                new Athlete { Id = new Guid("039C7EF0-B340-4715-AAF3-3082F83BF2D5"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Indonesia"), FullName = "TAMMY NG JIA WEN", PreferredName = "TAMMY NG JIA WEN"},
                new Athlete { Id = new Guid("64C238EC-6A17-44F7-BED9-36482FC08C32"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Singapore"), FullName = "IRIS HOO XIN EN", PreferredName = "IRIS HOO XIN EN"},
                new Athlete { Id = new Guid("63D0E2B8-7C17-4F30-B385-ABF11A7354DC"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Singapore"), FullName = "NUR AQMAR BINTI ANUAR", PreferredName = "NUR AQMAR ANNUAR"},
                new Athlete { Id = new Guid("857C7227-9CD2-4A85-8811-BBC7D09768A7"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Singapore"), FullName = "LOK MEI HUI", PreferredName = "LOK MEI HUI"},
                new Athlete { Id = new Guid("7AA0AF83-EF39-4DFA-82DB-5784EB21E2FC"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Singapore"), FullName = "CHEONG LEANN", PreferredName = "CHEONG LEANN"},
                new Athlete { Id = new Guid("D3AB7EAE-EC74-4DB0-B950-1F3DCD13F3F9"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Singapore"), FullName = "LOK MEI HUI", PreferredName = "LOK MEI HUI"},
                new Athlete { Id = new Guid("ADEBC309-776C-40F0-A851-E6C7C158F6D3"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Singapore"), FullName = "CHEONG LEANN", PreferredName = "CHEONG LEANN"},
                new Athlete { Id = new Guid("AFDFFE6A-41C0-4002-A26D-3E37EDA69831"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Timor-Leste"), FullName = "TAN WAN XI", PreferredName = "JOCELYN TAN WAN YI"},
                new Athlete { Id = new Guid("E696BD51-10B0-4DF3-9BB3-AAACDC189916"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Timor-Leste"), FullName = "HAN GAN YI", PreferredName = "JOCELYN TAN WAN YI"},
                new Athlete { Id = new Guid("BAC37EA6-22D8-437A-B73E-89B2B58BEA45"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Timor-Leste"), FullName = "AAN WAN WI", PreferredName = "JOCELYN TAN WAN YI"},
                new Athlete { Id = new Guid("A13F9CA0-C437-4039-85AC-BB463CBEB5F7"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Timor-Leste"), FullName = "BAN AAN BI", PreferredName = "JOCELYN TAN WAN YI"},
                new Athlete { Id = new Guid("49D9DCE0-3361-498D-BFB7-4EDCE99473A2"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Timor-Leste"), FullName = "MAN AAN AI", PreferredName = "JOCELYN TAN WAN YI"},
                new Athlete { Id = new Guid("D52C6236-4785-4249-A389-02A27DEB2D44"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Timor-Leste"), FullName = "PAN HAN TI", PreferredName = "JOCELYN TAN WAN YI"},
                new Athlete { Id = new Guid("833D5C0C-B266-449D-8816-677C5DF4CAD8"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Malaysia"), FullName = "EMILIE  NARA ZI YI JABU", PreferredName = "EMILIE NARA ZI YI J."},
                new Athlete { Id = new Guid("725C7F98-675F-42CD-942F-258C99A1D918"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Malaysia"), FullName = "BONNIE TAN YEE WEN", PreferredName = "BONNIE TAN YEE WEN"},
                new Athlete { Id = new Guid("2EFD3AEB-9840-4BEE-8ED1-A48C22DA0C3F"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Malaysia"), FullName = "RACHEL WANG QIAN YII", PreferredName = "RACHEL WANG QIAN YII"},
                new Athlete { Id = new Guid("5A746632-AFD9-46DD-B113-C000DFF4821A"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Malaysia"), FullName = "SARAH NG XI YAN", PreferredName = "SARAH NG XI YAN"},
                new Athlete { Id = new Guid("1B12F4A8-63EA-4AB5-97CD-1928E04B8369"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Cambodia"), FullName = "ANGELINA TEO JIA MEI", PreferredName = "ANGELINA TEO JIA MEI"},
                new Athlete { Id = new Guid("705BE6D8-8CF5-48F5-BE7B-A681BDC81F74"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Cambodia"), FullName = "ANTHONY", PreferredName = "ANTHONY"},
                new Athlete { Id = new Guid("69A622AF-5C81-4008-8713-C03671BD338F"), Gender = "M", Contingent = context.Contingents.First(c => c.Name == "Cambodia"), FullName = "ALBERT", PreferredName = "ALBERT"}
            );

            context.SaveChanges();

        }

        void SeedEventAthlete(ASEAContext context)
        {
            var e = context.Events.Find(Guid.Parse("B75A0E3F-4204-489E-80C0-6B6595339CAB"));
            e.Athletes.AddRange(context.Athletes);
            context.SaveChanges();
        }

        void ClearData(ASEAContext context)
        {
            context.Rankings.RemoveRange(context.Rankings);
            context.Result.RemoveRange(context.Result);
            context.Matches.RemoveRange(context.Matches);
            context.SaveChanges();
        }
    }
}
