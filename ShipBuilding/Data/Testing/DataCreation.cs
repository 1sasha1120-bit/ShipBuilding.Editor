using ShipBuilding.Data.Interfaces;
using ShipBuilding.Entities;
using ShipBuilding.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBuilding.Data.Testing
{
    public static class DataCreation
    {
        public static bool CreateTestingData(this IDataSet dataSet)
        {
            if (dataSet.IsEmpty())
            {
                CreateTestingTypeOfShips(dataSet.Types);
                CreateTestingShipyards(dataSet.Shipyards);
                CreateTestingShipProjects(dataSet);
                CreateTestingShips(dataSet);
                return true;
            }
            return false;
        }

        public static void CreateTestingShipyards(this ICollection<Shipyard> shipyards)
        {
            shipyards.Add(new Shipyard("Chantiers de l'Atlantique",
               "Франція", 1862, 4, 420000)
            { Id = 1 });

            shipyards.Add(new Shipyard("Imabari Shipbuilding Co., Ltd.",
                "Японія", 1901, 9, 2000000)
            { Id = 2 });

            shipyards.Add(new Shipyard("Fincantieri",
                "Італія", 1959, 10, 2200000)
            { Id = 3 });

            shipyards.Add(new Shipyard("Samsung Heavy Industries",
                "Південна Корея", 1974, 7, 2500000)
            { Id = 4 });

            shipyards.Add(new Shipyard("Meyer Werft",
                "Німеччина", 1795, 2, 300000)
            { Id = 5 });

            shipyards.Add(new Shipyard("Hanwha Ocean (DSME)",
                "Південна Корея", 1973, 2, 2800000)
            { Id = 6 });

            shipyards.Add(new Shipyard("Hyundai Heavy Industries",
                "Південна Корея", 1972, 11, 3500000)
            { Id = 7 });

            shipyards.Add(new Shipyard("Damen Shipyards",
                "Нідерланди", 1927, 6, 150000)
            { Id = 8 });

            shipyards.Add(new Shipyard("Суднобудівний завод «Океан»",
                "Україна", 1951, 3, 250000)
            { Id = 9 });

            shipyards.Add(new Shipyard("Чорноморський суднобудівний завод",
                "Україна", 1897, 4, 180000)
            { Id = 10 });

            // Завод з неповними даними
            shipyards.Add(new Shipyard("Kherson Shipyard",
                "Україна", null, null, null)
            { Id = 11 });
        }

        public static void CreateTestingTypeOfShips(this ICollection<TypeOfShip> types)
        {
            types.Add(new TypeOfShip("Судно", null, null)
            {
                Id = 1,
                Description = "Судно — це будь-який плавучий засіб, призначений або " +
                "фактично використаний для пересування по воді. Відповідно до " +
                "Міжнародних конвенцій (зокрема SOLAS 1974, MARPOL 1973/78, " +
                "Конвенції ООН з морського права 1982 року (UNCLOS, 1982)), " +
                "термін охоплює плавучі засоби будь-якого типу, що діють у морському " +
                "середовищі, включаючи військові кораблі, катери, баржі, підводні апарати, " +
                "судна на повітряній подушці, гідролітаки, а також стаціонарні або плавучі платформи."
            });

            types.Add(new TypeOfShip("Транспортний",
                types.FirstOrDefault(e => e.Name == "Судно"), "TRS")
            {
                Id = 2,
                Description = "Транспортний судно — судно, призначене для перевезення " +
                "вантажів або пасажирів. Воно може бути різних типів, таких як " +
                "контейнеровоз, танкер, пасажирський лайнер, і оснащене для безпечного " +
                "і ефективного транспортування."
            });

            types.Add(new TypeOfShip("Промисловий",
                types.FirstOrDefault(e => e.Name == "Судно"), "IND")
            {
                Id = 3,
                Description = "Промисловий судно — судно, призначене для виконання " +
                "спеціалізованих завдань у промисловості, таких як буріння нафтових " +
                "вишок, дослідження океану або рятувальні операції. Воно оснащене " +
                "спеціальним обладнанням для виконання своїх функцій."
            });

            types.Add(new TypeOfShip("Науково-дослідне",
                types.FirstOrDefault(e => e.Name == "Судно"), "RND")
            {
                Id = 4,
                Description = "Науково-дослідне судно — судно, призначене для проведення " +
                "наукових досліджень у морських умовах. Воно оснащене лабораторіями, " +
                "науковим обладнанням і засобами для збору даних про океан, клімат, " +
                "морську біологію та інші аспекти морського середовища."
            });

            types.Add(new TypeOfShip("Навчальний",
                types.FirstOrDefault(e => e.Name == "Судно"), "EDU")
            {
                Id = 5,
                Description = "Навчальний судно — судно, призначене для навчання і практики " +
                "студентів морських академій та інших навчальних закладів. Воно оснащене " +
                "для проведення навчальних курсів з навігації, морської інженерії та інших " +
                "аспектів морської справи."
            });

            types.Add(new TypeOfShip("Спеціальний",
                types.FirstOrDefault(e => e.Name == "Судно"), "SPC")
            {
                Id = 6,
                Description = "Спеціальний судно — судно, призначене для виконання " +
                "специфічних завдань, таких як рятувальні операції, патрулювання, " +
                "пожежогасіння або інші спеціалізовані функції. Воно оснащене " +
                "відповідним обладнанням для виконання своїх завдань."
            });

            types.Add(new TypeOfShip("Судноремонтний",
                types.FirstOrDefault(e => e.Name == "Судно"), "AR")
            {
                Id = 7,
                Description = "Судноремонтний судно — судно, призначене для ремонту та обслуговування " +
                "інших суден. Воно оснащене підйомними механізмами, ремонтними майстернями " +
                "та іншими засобами для виконання ремонтних робіт на воді або в порту."
            });

            types.Add(new TypeOfShip("Службовий",
                types.FirstOrDefault(e => e.Name == "Судно"), "SRV")
            {
                Id = 8,
                Description = "Службовий судно — судно, призначене для виконання допоміжних " +
                "функцій, таких як буксирування, забезпечення безпеки, патрулювання " +
                "або інші служби, що підтримують морську діяльність. Воно оснащене " +
                "відповідним обладнанням для виконання своїх завдань."
            });

            //----------------------------------------------------------------------------------------

            types.Add(new TypeOfShip("Пасажирське",
                types.FirstOrDefault(e => e.Name == "Транспортний"), "PASS")
            {
                Id = 9,
                Description = "Пасажирське судно — судно, призначене для перевезення " +
                "людей. Оснащене каютами, салонами та засобами безпеки для комфортного " +
                "і безпечного перевезення пасажирів."
            });

            types.Add(new TypeOfShip("Вантажне",
                types.FirstOrDefault(e => e.Name == "Транспортний"), "CARGO")
            {
                Id = 10,
                Description = "Вантажне судно — судно, призначене для перевезення різних видів " +
                "вантажів, таких як контейнери, нафта, хімікати або інші матеріали. Оснащене " +
                "вантажними трюмами, кранами або іншими засобами для завантаження і розвантаження вантажу."
            });

            //----------------------------------------------------------------------------------------

            types.Add(new TypeOfShip("Риболовецький",
                types.FirstOrDefault(e => e.Name == "Промисловий"), "FSH")
            {
                Id = 11,
                Description = "Риболов судно — судно, призначене для комерційного вилову риби та інших морських " +
                "морепродуктів. Оснащене риболовними знаряддями, холодильними камерами та іншими " +
                "засобами для ефективного вилову та зберігання риби."
            });


            types.Add(new TypeOfShip("Експедиційний",
                types.FirstOrDefault(e => e.Name == "Науково-дослідне"), "EXP")
            {
                Id = 12,
                Description = "Експедиційний судно — судно, призначене для проведення наукових " +
                "експедицій у віддалені або важкодоступні райони океану. Оснащене лабораторіями, " +
                "науковим обладнанням та засобами для збору даних про морське середовище, клімат, " +
                "біологію та інші аспекти океану."
            });

            types.Add(new TypeOfShip("Водолазний",
                                types.FirstOrDefault(e => e.Name == "Науково-дослідне"), "DIV")
            {
                Id = 13,
                Description = "Водолазний судно — судно, призначене для підтримки водолазних операцій. " +
                "Оснащене декомпресійними камерами, водолазним обладнанням та іншими ресурсами для " +
                "забезпечення безпеки та ефективності водолазних робіт на морі."
            });

            types.Add(new TypeOfShip("Патрульний",
                types.FirstOrDefault(e => e.Name == "Спеціальний"), "PAT")
            {
                Id = 14,
                Description = "Патрульний судно — судно, призначене для патрулювання морських територій, " +
                "забезпечення безпеки, боротьби з контрабандою, риболовними порушеннями та іншими " +
                "правопорушеннями на воді. Оснащене відповідним обладнанням для виконання своїх задач."
            });

            types.Add(new TypeOfShip("Криголами",
                types.FirstOrDefault(e => e.Name == "Службовий"), "ICE")
            {
                Id = 15,
                Description = "Криголам — судно, призначене для розбиття льоду та забезпечення проходу " +
                "інших суден через замерзлі води. Оснащене посиленим корпусом, спеціальними " +
                "криголамними носами та потужними двигунами для ефективного розбиття льоду."
            });

            types.Add(new TypeOfShip("Буксир",
                types.FirstOrDefault(e => e.Name == "Службовий"), "TUG")
            {
                Id = 16,
                Description = "Буксир — судно, призначене для буксирування або маневрування інших суден, " +
                "особливо в портах або вузьких водах. Оснащене потужними двигунами та спеціальними " +
                "засобами для захоплення та утримання інших суден під час буксирування."
            });

            //----------------------------------------------------------------------------------------

            types.Add(new TypeOfShip("Лайнер",
                types.FirstOrDefault(e => e.Name == "Пасажирське"), "CX")
            {
                Id = 17,
                Description = "Лайнер — судно, призначене для перевезення пасажирів на великі відстані. " +
                "Оснащене розкішними каютами, ресторанами, розважальними закладами та іншими " +
                "зручностями для забезпечення комфортного подорожування."
            });

            types.Add(new TypeOfShip("Круїзне",
                types.FirstOrDefault(e => e.Name == "Пасажирське"), "CR")
            {
                Id = 18,
                Description = "Круїзне судно — судно, призначене для організації круїзів та розважальних " +
                "подорожей. Оснащене різноманітними зручностями, такими як басейни, театри, ресторани " +
                "та інші розважальні заклади для забезпечення незабутнього відпочинку на воді."
            });

            types.Add(new TypeOfShip("Танкер",
                types.FirstOrDefault(e => e.Name == "Транспортний"), "TANK")
            {
                Id = 19,
                Description = "Танкер — судно, призначене для перевезення рідких вантажів, " +
                "таких як нафта, хімікати або зріджений газ. Оснащене спеціальними " +
                "резервуарами та системами безпеки для безпечного транспортування рідин."
            });
            types.Add(new TypeOfShip("Контейнеровоз",
                types.FirstOrDefault(e => e.Name == "Транспортний"), "CON")
            {
                Id = 20,
                Description = "Контейнеровоз — судно, призначене для перевезення вантажів у контейнерах. " +
                "Оснащене спеціальними конструкціями для фіксації контейнерів та забезпечення " +
                "їх безпеки під час транспортування."
            });

            types.Add(new TypeOfShip("Сейнери",
                types.FirstOrDefault(e => e.Name == "Риболовецький"), "SN")
            {
                Id = 21,
                Description = "Сейнер — це спеціалізоване рибопромислове судно, призначене для " +
                "вилову риби кошелевим неводом (великою сіткою) або снюрреводом."
            });

            types.Add(new TypeOfShip("Траулер",
                types.FirstOrDefault(e => e.Name == "Риболовецький"), "TRW")
            {
                Id = 22,
                Description = "Траулер — це тип риболовного судна, призначеного для вилову риби " +
                "за допомогою трала — великої сітки, що буксирується позаду судна. Траулери " +
                "можуть бути оснащені різними типами тралів, такими як донні або пелагічні, " +
                "для вилову різних видів риби."
            });

            types.Add(new TypeOfShip("Пором",
                types.FirstOrDefault(e => e.Name == "Пасажирське"), "FERRY")
            {
                Id = 23,
                Description = "Пором — судно для регулярного перевезення пасажирів і транспортних " +
                "засобів на коротких лініях між портами."
            });

            types.Add(new TypeOfShip("Газовоз",
                types.FirstOrDefault(e => e.Name == "Танкер"), "LNG")
            {
                Id = 24,
                Description = "Газовоз — танкер для перевезення зрідженого природного газу (LNG) " +
                "у кріогенних резервуарах."
            });

        }

        public static void CreateTestingShipProjects(this IDataSet dataSet)
        {
            dataSet.ShipProjects.Add(new ShipProject
            {
                Id = 1,
                Name = "Project Queen",
                ProjectCode = "SP-2000-001",
                Description = "Проєкт будівництва флагманського океанського лайнера для Cunard Line.",
                Budget = 800_000_000m,
                Shipyard = dataSet.Shipyards.First(e => e.Name == "Chantiers de l'Atlantique")
            });

            dataSet.ShipProjects.Add(new ShipProject
            {
                Id = 2,
                Name = "Project Horizon",
                ProjectCode = "SP-2015-002",
                Description = "Проєкт будівництва серії надвеликих контейнеровозів класу Ever.",
                Budget = 185_000_000m,
                Shipyard = dataSet.Shipyards.First(e => e.Name == "Imabari Shipbuilding Co., Ltd.")
            });

            dataSet.ShipProjects.Add(new ShipProject
            {
                Id = 3,
                Name = "Project Adriatica",
                ProjectCode = "SP-2004-003",
                Description = "Проєкт круїзного судна для середземноморських маршрутів Costa Cruises.",
                Budget = 420_000_000m,
                Shipyard = dataSet.Shipyards.First(e => e.Name == "Fincantieri")
            });

            dataSet.ShipProjects.Add(new ShipProject
            {
                Id = 4,
                Name = "Project Sejong",
                ProjectCode = "SP-2019-004",
                Description = "Проєкт будівництва великого танкера VLCC для транспортування нафти.",
                Budget = 110_000_000m,
                Shipyard = dataSet.Shipyards.First(e => e.Name == "Samsung Heavy Industries")
            });

            Shipyard Yard(string name) => dataSet.Shipyards.First(e => e.Name == name);

            dataSet.ShipProjects.Add(new ShipProject
            {
                Id = 5,
                Name = "Project Aurora",
                ProjectCode = "SP-2016-005",
                Description = "Серія круїзних суден на зрідженому газі (LNG) для європейських маршрутів.",
                Budget = 950_000_000m,
                Shipyard = Yard("Meyer Werft")
            });

            dataSet.ShipProjects.Add(new ShipProject
            {
                Id = 6,
                Name = "Project Arctic",
                ProjectCode = "SP-2014-006",
                Description = "Криголамні газовози арктичного класу для проєкту Yamal LNG.",
                Budget = 320_000_000m,
                Shipyard = Yard("Hanwha Ocean (DSME)")
            });

            dataSet.ShipProjects.Add(new ShipProject
            {
                Id = 7,
                Name = "Project Seawolf",
                ProjectCode = "SP-2020-007",
                Description = "Серія портових буксирів-штовхачів з азимутальними рушіями.",
                Budget = 24_500_000m,
                Shipyard = Yard("Damen Shipyards")
            });

            dataSet.ShipProjects.Add(new ShipProject
            {
                Id = 8,
                Name = "Project Чорноморець",
                ProjectCode = "SP-1984-008",
                Description = "Риболовецькі траулери та сейнери для Чорноморського басейну.",
                Budget = 45_000_000m,
                Shipyard = Yard("Суднобудівний завод «Океан»")
            });

            // Проєкти без суден
            dataSet.ShipProjects.Add(new ShipProject
            {
                Id = 9,
                Name = "Project Neptune",
                ProjectCode = "SP-2024-009",
                Description = "Контейнеровоз нового покоління місткістю 24 000 TEU на метаноловому паливі.",
                Budget = 650_000_000m,
                Shipyard = Yard("Hyundai Heavy Industries")
            });

            dataSet.ShipProjects.Add(new ShipProject
            {
                Id = 10,
                Name = "Project Green Ferry",
                ProjectCode = "SP-2025-010",
                Description = "Електричний пором для коротких прибережних ліній (бюджет ще не затверджено).",
                Budget = null,
                Shipyard = Yard("Damen Shipyards")
            });

            dataSet.ShipProjects.Add(new ShipProject
            {
                Id = 11,
                Name = "Project Polaris",
                ProjectCode = null,
                Description = "Концепт науково-дослідного судна для антарктичних експедицій.",
                Budget = null,
                Shipyard = null
            });
        }

        public static void CreateTestingShips(this IDataSet dataSet)
        {
            dataSet.Ships.Add(new Ship(
                "Queen Mary 2", "9241061",
                new DateTime(2003, 4, 21),
                ShipBuilding.Types.ShipStatus.InService,
                72.0, 41.0, 345.0,
                dataSet.Shipyards.First(e => e.Name == "Chantiers de l'Atlantique"),
                dataSet.Types.FirstOrDefault(e => e.Name == "Лайнер"),
                dataSet.ShipProjects.FirstOrDefault(e => e.Name == "Project Queen"))
            { Id = 1 });

            dataSet.Ships.Add(new Ship(
                "Ever Given", "9811000",
                new DateTime(2018, 6, 9),
                ShipBuilding.Types.ShipStatus.InService,
                32.9, 58.8, 399.9,
                dataSet.Shipyards.First(e => e.Name == "Imabari Shipbuilding Co., Ltd."),
                dataSet.Types.FirstOrDefault(e => e.Name == "Контейнеровоз"),
                dataSet.ShipProjects.FirstOrDefault(e => e.Name == "Project Horizon"))
            { Id = 2 });

            dataSet.Ships.Add(new Ship(
                "Costa Serena", "9317109",
                new DateTime(2007, 3, 31),
                ShipBuilding.Types.ShipStatus.InService,
                63.0, 35.5, 290.2,
                dataSet.Shipyards.First(e => e.Name == "Fincantieri"),
                dataSet.Types.FirstOrDefault(e => e.Name == "Круїзне"),
                dataSet.ShipProjects.FirstOrDefault(e => e.Name == "Project Adriatica"))
            { Id = 3 });

            dataSet.Ships.Add(new Ship(
                "Sejong the Great", "9348150",
                new DateTime(2007, 5, 22),
                ShipBuilding.Types.ShipStatus.InService,
                null, 21.4, 165.9,
                dataSet.Shipyards.First(e => e.Name == "Samsung Heavy Industries"),
                dataSet.Types.FirstOrDefault(e => e.Name == "Патрульний"),
                null)
            { Id = 4 });

            dataSet.Ships.Add(new Ship(
                "Ramform Titan", "9642710",
                new DateTime(2013, 10, 7),
                ShipBuilding.Types.ShipStatus.InService,
                null, 70.0, 104.2,
                dataSet.Shipyards.First(e => e.Name == "Samsung Heavy Industries"),
                dataSet.Types.FirstOrDefault(e => e.Name == "Вантажне"),
                null)
            { Id = 5 });

            dataSet.Ships.Add(new Ship(
                "TI Europe", "9218482", 
                new DateTime(2002, 9, 14),
                ShipBuilding.Types.ShipStatus.InService,
                30.0, 68.0, 380.0,
                dataSet.Shipyards.First(e => e.Name == "Samsung Heavy Industries"),
                dataSet.Types.FirstOrDefault(e => e.Name == "Танкер"),
                dataSet.ShipProjects.FirstOrDefault(e => e.Name == "Project Sejong"))
            { Id = 6 });

            dataSet.Ships.Add(new Ship(
                "MSC Oscar", "9703291",
                new DateTime(2014, 12, 8),
                ShipBuilding.Types.ShipStatus.InService,
                30.5, 55.0, 395.4,
                dataSet.Shipyards.First(e => e.Name == "Imabari Shipbuilding Co., Ltd."),
                dataSet.Types.FirstOrDefault(e => e.Name == "Контейнеровоз"),
                dataSet.ShipProjects.FirstOrDefault(e => e.Name == "Project Horizon"))
            { Id = 7 });

            dataSet.Ships.Add(new Ship(
                "Symphony of the Seas", "9744001",
                new DateTime(2018, 3, 23),
                ShipBuilding.Types.ShipStatus.InService,
                72.0, 66.0, 362.1,
                dataSet.Shipyards.First(e => e.Name == "Imabari Shipbuilding Co., Ltd."),
                dataSet.Types.FirstOrDefault(e => e.Name == "Круїзне"),
                dataSet.ShipProjects.FirstOrDefault(e => e.Name == "Project Adriatica"))
            { Id = 8 });

            Shipyard? Yard(string name) => dataSet.Shipyards.FirstOrDefault(e => e.Name == name);
            TypeOfShip? Type(string name) => dataSet.Types.FirstOrDefault(e => e.Name == name);
            ShipProject? Project(string name) => dataSet.ShipProjects.FirstOrDefault(e => e.Name == name);

            // — великі судна світових верфей
            dataSet.Ships.Add(new Ship("AIDAnova", "9781865", new DateTime(2018, 8, 24),
                ShipStatus.InService, 62.0, 42.0, 337.0,
                Yard("Meyer Werft"), Type("Круїзне"), Project("Project Aurora"))
            { Id = 9 });

            dataSet.Ships.Add(new Ship("Costa Smeralda", "9781889", new DateTime(2019, 4, 13),
                ShipStatus.UnderRepair, 62.0, 42.0, 337.0,
                Yard("Meyer Werft"), Type("Круїзне"), Project("Project Aurora"))
            { Id = 10 });

            dataSet.Ships.Add(new Ship("Christophe de Margerie", "9737187", new DateTime(2016, 1, 18),
                ShipStatus.InService, 52.0, 50.0, 299.0,
                Yard("Hanwha Ocean (DSME)"), Type("Газовоз"), Project("Project Arctic"))
            { Id = 11 });

            dataSet.Ships.Add(new Ship("Boris Vilkitsky", "9768368", new DateTime(2017, 3, 2),
                ShipStatus.InReserve, 52.0, 50.0, 299.0,
                Yard("Hanwha Ocean (DSME)"), Type("Газовоз"), Project("Project Arctic"))
            { Id = 12 });

            dataSet.Ships.Add(new Ship("HMM Algeciras", "9863297", new DateTime(2020, 4, 23),
                ShipStatus.InService, 33.2, 61.0, 399.9,
                Yard("Hanwha Ocean (DSME)"), Type("Контейнеровоз"), null)
            { Id = 13 });

            dataSet.Ships.Add(new Ship("Harmony of the Seas", "9682875", new DateTime(2015, 6, 19),
                ShipStatus.InService, 70.0, 66.0, 362.1,
                Yard("Chantiers de l'Atlantique"), Type("Круїзне"), null)
            { Id = 14 });

            // — списані / історичні (завод-будівельник відсутній у довіднику)
            dataSet.Ships.Add(new Ship("Queen Elizabeth 2", "6725418", new DateTime(1967, 9, 20),
                ShipStatus.Decommissioned, 52.0, 32.0, 293.5,
                null, Type("Лайнер"), null)
            { Id = 15 });

            dataSet.Ships.Add(new Ship("Seawise Giant", "7381154", new DateTime(1979, 12, 1),
                ShipStatus.Decommissioned, 29.8, 68.8, 458.5,
                null, Type("Танкер"), null)
            { Id = 16 });

            // — буксири Damen
            dataSet.Ships.Add(new Ship("Атлас", "9900101", new DateTime(2021, 5, 14),
                ShipStatus.InService, 12.5, 11.3, 28.2,
                Yard("Damen Shipyards"), Type("Буксир"), Project("Project Seawolf"))
            { Id = 17 });

            dataSet.Ships.Add(new Ship("Геркулес", "9900102", new DateTime(2022, 2, 3),
                ShipStatus.UnderRepair, 12.5, 11.3, 28.2,
                Yard("Damen Shipyards"), Type("Буксир"), Project("Project Seawolf"))
            { Id = 18 });

            // — українські заводи
            dataSet.Ships.Add(new Ship("Скумбрія", "8711002", new DateTime(1987, 7, 11),
                ShipStatus.InReserve, 14.0, 13.6, 61.0,
                Yard("Суднобудівний завод «Океан»"), Type("Траулер"), Project("Project Чорноморець"))
            { Id = 19 });

            dataSet.Ships.Add(new Ship("Таврія", "8711003", new DateTime(1985, 4, 30),
                ShipStatus.Decommissioned, null, 9.8, 38.5,
                Yard("Суднобудівний завод «Океан»"), Type("Сейнери"), Project("Project Чорноморець"))
            { Id = 20 });

            dataSet.Ships.Add(new Ship("Нептун", "9512004", new DateTime(2011, 10, 5),
                ShipStatus.InService, 18.0, 10.2, 62.0,
                Yard("Чорноморський суднобудівний завод"), Type("Патрульний"), null)
            { Id = 21 });

            dataSet.Ships.Add(new Ship("Курсант", "9205005", new DateTime(1993, 6, 18),
                ShipStatus.InService, 35.0, 12.0, 95.0,
                Yard("Чорноморський суднобудівний завод"), Type("Навчальний"), null)
            { Id = 22 });

            dataSet.Ships.Add(new Ship("Ноосфера", "7432175", new DateTime(1976, 3, 12),
                ShipStatus.InService, null, 15.2, 87.0,
                null, Type("Експедиційний"), null)
            { Id = 23 });

            dataSet.Ships.Add(new Ship("Крижаний", "9301006", new DateTime(2004, 11, 20),
                ShipStatus.InReserve, 20.0, 18.5, 75.0,
                Yard("Kherson Shipyard"), Type("Криголами"), null)
            { Id = 24 });

            // — записи з неповними даними (для перевірки порожніх полів)
            dataSet.Ships.Add(new Ship("Ремонтник-1", null, null,
                ShipStatus.Unknown, null, null, null,
                Yard("Чорноморський суднобудівний завод"), Type("Судноремонтний"), null)
            { Id = 25 });

            dataSet.Ships.Add(new Ship("Без назви №7", "0000007", null,
                ShipStatus.Unknown, null, null, null,
                null, null, null)
            { Id = 26 });
        }
    }
}
