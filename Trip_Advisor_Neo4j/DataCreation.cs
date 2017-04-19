using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trip_Advisor_Neo4j.DomainModel;
using Trip_Advisor_Neo4j.DataAccess;
using Neo4jClient.Cypher;
using Neo4jClient;
using System.IO;

/// <summary>
/// klosarska klasa koja sluzi za kreiranje test podataka
/// chore: zameni opise i imena na srpskom engleskim verzijama da ne bude klosarski pola pola - PRE PREZENTACIJE
/// </summary>
namespace Trip_Advisor_Neo4j
{
    public partial class DataCreation : Form
    {
        public static int mitarId;
        public static int vojaId;

        public DataCreation()
        {
            InitializeComponent();
            DataLayer.Connect();

        }

        public string defaultUserPicture = "/Content/Images/User-Default.jpg";

        public int[] userIds = new int[15];


        private void create_data_Click(object sender, EventArgs e)
        {
            // pri kreiranju nove baze postaviti sifru specificiranu u DataLayer.Connect()
            DataProviderCreate.CreateIdNodes();

            System.Threading.Thread.Sleep(150);

            

            User u1 = new User();
            u1.Username = "Perica";
            u1.UserStatusFLAG = 1;
            u1.Email = u1.Username + "@gmail.com";
            u1.Password = "123";
            u1.ProfilePicture = this.defaultUserPicture;
            DataProviderCreate.CreateUser(u1);
            int perica = Int32.Parse(DataProviderGet.GetMaxId("User"));
       
            u1.Username = "Mitar";
            u1.UserStatusFLAG = 1;
            u1.Password = "123";
            u1.Email = u1.Username + "@gmail.com";
            u1.ProfilePicture = this.defaultUserPicture;
            DataProviderCreate.CreateUser(u1);
            int mitar = Int32.Parse(DataProviderGet.GetMaxId("User"));
            mitarId = mitar;

            u1.Username = "Pujo";
            u1.Password = "123";
            u1.UserStatusFLAG = 1;
            u1.Email = u1.Username + "@gmail.com";
            u1.ProfilePicture = this.defaultUserPicture;
            DataProviderCreate.CreateUser(u1);
            int pujo = Int32.Parse(DataProviderGet.GetMaxId("User"));

            u1.Username = "Stojan";
            u1.Password = "123";
            u1.UserStatusFLAG = 9;
            u1.Email = u1.Username + "@gmail.com";
            u1.ProfilePicture = this.defaultUserPicture;
            DataProviderCreate.CreateUser(u1);
            int stojan = Int32.Parse(DataProviderGet.GetMaxId("User"));


            u1.Username = "TonusHleb";
            u1.Password = "123";
            u1.UserStatusFLAG = 1;
            u1.Email = u1.Username + "@gmail.com";
            u1.ProfilePicture = this.defaultUserPicture;
            DataProviderCreate.CreateUser(u1);
            int tonushleb = Int32.Parse(DataProviderGet.GetMaxId("User"));


            u1.Username = "Vojislav";
            u1.Password = "123";
            u1.UserStatusFLAG = 9;
            u1.Email = u1.Username + "@gmail.com";
            u1.ProfilePicture = this.defaultUserPicture;
            DataProviderCreate.CreateUser(u1);
            int voja = Int32.Parse(DataProviderGet.GetMaxId("User"));
            vojaId = voja;


            u1.Username = "Vucko";
            u1.Password = "123";
            u1.Email = u1.Username + "@gmail.com";
            u1.UserStatusFLAG = 1;
            u1.ProfilePicture = this.defaultUserPicture;
            DataProviderCreate.CreateUser(u1);
            int vucko = Int32.Parse(DataProviderGet.GetMaxId("User"));


            u1.Username = "admin";
            u1.Password = "admin";
            u1.Email = u1.Username + "@gmail.com";
            u1.UserStatusFLAG = 10;
            u1.ProfilePicture = this.defaultUserPicture;
            DataProviderCreate.CreateUser(u1);
            int admin = Int32.Parse(DataProviderGet.GetMaxId("User"));


         

            Place p = new Place();

            p.Name = "Đavolja Varoš";
            //p.Pictures = this.CreatePlacePictureList("DjavoljaVaros", 4);
            p.CityCenterDistance = 90;
            p.Rating = 0.0f;
            p.Latitude = 43.0106f;
            p.Longitude = 21.4089f;
            p.Description = "Đavolja Varoš je vrlo atraktivan prirodni fenomen koji se nalazi na jugu Srbije, oko 90 km jugozapadno od Niša. Čine je 202 kamene figure koje su nastale dugotrajnim i strpljivim radom prirode";
            DataProviderCreate.CreatePlace(p);
            int djavolja_varos = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Spomen-park „Kragujevački oktobar“";
            p.CityCenterDistance = 14;
            //p.Pictures = this.CreatePlacePictureList("Sumarice", 4);
            p.Rating = 0.0f;
            p.Latitude = 44.0159f;
            p.Longitude = 20.8796f;
            p.Description = "Spomen-park „Kragujevački oktobar“, predstavlja spomen kompleks podignut u znak sećanja na nedužne žrtve Kragujevačkog masakra koji su počinili pripadnici Vermahta 21. oktobra 1941. godine. Tog dana nemački okupatori su u Šumaricama streljali oko 3.000 stanovnika Kragujevca i okolnih mesta, a među njima je bilo i 300 učenika kragujevačkih srednjih škola i šegrta koji su već radili, kao i 15 dečaka, čistača obuće, od 12 do 15 godina starosti.";
            DataProviderCreate.CreatePlace(p);
            int sumarice = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Ćele-kula";
            //p.Pictures = this.CreatePlacePictureList("CeleKula", 4);
            p.CityCenterDistance = 4;
            p.Rating = 0.0f;
            p.Latitude = 43.3121f;
            p.Longitude = 21.9239f;
            p.Description = "Ćele-kula (tur. kelle kulesi, \"kula od lobanja\") je spomenik iz Prvog srpskog ustanka koji je u znak odmazde tadašnja Turska vlast u Srbiji izgradila od lobanja, poginulih srpskih ratnika, predvođenih Stevanom Sinđelićem, u bici na Čegru. Nalazi se na 4 km od centra Niša, na putu ka Niškoj Banji. Svrstana je u spomenike kulture od izuzetnog značaja za Republiku Srbiju i danas predstavlja muzejski objekat.";
            DataProviderCreate.CreatePlace(p);
            int cele_kula = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Medijana";
            //p.Pictures = this.CreatePlacePictureList("Medijana", 4);
            p.CityCenterDistance = 12;
            p.Rating = 0.0f;
            p.Latitude = 43.3098f;
            p.Longitude = 21.9485f;
            p.Description = "Medijana, rimska Medijana (lat. Mediana) danas arheološki park u istočnom delu Niša, na pola puta za Nišku Banju, u vreme vladavine cara Konstantina Velikog, rimsko naselje ili kompleks letnjih rezidencija tipa urbanih vila i veliko poljoprivredno gazdinstvo, otvorenog tipa, pokraj puta, koji je od Naisa vodio ka istoku, prema Serdici i dalje prema Konstantinopolisu. Naselje je nastalo na samom kraju 3. ili početkom 4. veka.";
            DataProviderCreate.CreatePlace(p);
            int medijana = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Hram Svetog cara Konstantina i carice Jelene";
            p.CityCenterDistance = 13;
            p.Rating = 0.0f;
            p.Latitude = 43.3205f;
            p.Longitude = 21.9198f;
            p.Description = "Hram Svetog cara Konstantina i carice Jelene je hram Srpske pravoslavne crkve koji se nalazi u novom delu grada Niša, a koji je grad Niš posvetio znamenitom Nišliji Konstantinu Velikom i njegovoj majci Jeleni.Hram se nalazi u jednom od najvećih niških parkova, parku Svetog Save. Projektant hrama je Jovan Mandić. Hram je izgrađen u vizantijskom stilu sa dodatkom dva zvonika.";
            DataProviderCreate.CreatePlace(p);
            int hramSCK = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Spomen-park Bubanj";
            //p.Pictures = this.CreatePlacePictureList("Bubanj", 4);
            p.CityCenterDistance = 19;
            p.Rating = 0.0f;
            p.Latitude = 43.3053f;
            p.Longitude = 21.8726f;
            p.Description = "Spomen park Bubanj predstavlja spomen kompleks, sagrađen u sećanje na streljane građane Niša i južne Srbije u Drugom svetskom ratu i nalazi se jugozapadno od Niša, u niškoj opštini Palilula.] Spomen park Bubanj je kao autentično mesto masovnog fašističkog terora, stavljen pod zaštitu države u maju 1973 godine. Dok je aprila 1979. godine odlukom Skupštine Srbije spomen park Bubanj proglašen kulturnim dobrom od izuzetnog značaja";
            DataProviderCreate.CreatePlace(p);
            int bubanj = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            //italijanska mesta
            //Venecija
            p.Name = "Basilica Cattedrale Patriarcale di San Marco";
            p.CityCenterDistance = 5;
            p.Rating = 0.0f;
            p.Latitude = 45.4346f;
            p.Longitude = 12.3397f;
            p.Description = "The Patriarchal Cathedral Basilica of Saint Mark (Italian: Basilica Cattedrale Patriarcale di San Marco), commonly known as Saint Marks Basilica (Italian: Basilica di San Marco; Venetian: Baxéłega de San Marco), is the cathedral church of the Roman Catholic Archdiocese of Venice, northern Italy. It is the most famous of the citys churches and one of the best known examples of Italo-Byzantine architecture. It lies at the eastern end of the Piazza San Marco, adjacent and connected to the Doges Palace.";
            DataProviderCreate.CreatePlace(p);
            int sanMarcoBas = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Piazza San Marco";
            p.CityCenterDistance = 5;
            p.Rating = 0.0f;
            p.Latitude = 45.4342f;
            p.Longitude = 12.3385f;
            p.Description = "Piazza San Marco ( Venetian: Piasa San Marco), often known in English as St Marks Square, is the principal public square of Venice, Italy, where it is generally known just as la Piazza (\"the Square\"). All other urban spaces in the city (except the Piazzetta and the Piazzale Roma) are called campi (\"fields\"). The Piazzetta (\"little Piazza |Square\") is an extension of the Piazza towards the lagoon in its south east corner (see plan). The two spaces together form the social, religious and political centre of Venice and are commonly considered together. This article relates to both of them.";
            DataProviderCreate.CreatePlace(p);
            int piazzaSM = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            //Milano
            p.Name = "Piazza del Duomo";
            p.CityCenterDistance = 5;
            p.Rating = 0.0f;
            p.Latitude = 45.4642f;
            p.Longitude = 9.1899f;
            p.Description = "Piazza del Duomo (\"Cathedral Square\") is the main piazza (city square) of Milan, Italy. It is named after, and dominated by, the Milan Cathedral (the Duomo). The piazza marks the center of the city, both in a geographic sense and because of its importance from an artistic, cultural, and social point of view. Rectangular in shape, with an overall area of 17,000 m2 (about 183,000 sq ft), the piazza includes some of the most important buildings of Milan (and Italy in general), as well some of the most prestigious commercial activities, and it is by far the foremost tourist attraction of the city.";
            DataProviderCreate.CreatePlace(p);
            int piazzaDD = Int32.Parse(DataProviderGet.GetMaxId("Place"));


            //Rim
            p.Name = "Coliseum";
            p.CityCenterDistance = 4;
            p.Rating = 0.0f;
            p.Latitude = 41.8902f;
            p.Longitude = 12.4922f;
            p.Description = "The Colosseum or Coliseum, also known as the Flavian Amphitheatre (Latin: Amphitheatrum Flavium; Italian: Anfiteatro Flavio [aŋfiteˈaːtro ˈflaːvjo] or Colosseo [kolosˈsɛːo]), is an oval amphitheatre in the centre of the city of Rome, Italy. Built of concrete and sand,[1] it is the largest amphitheatre ever built. The Colosseum is situated just east of the Roman Forum. Construction began under the emperor Vespasian in AD 72,[2] and was completed in AD 80 under his successor and heir Titus.[3] Further modifications were made during the reign of Domitian (81–96).[4] These three emperors are known as the Flavian dynasty, and the amphitheatre was named in Latin for its association with their family name (Flavius).";
            DataProviderCreate.CreatePlace(p);
            int colosseum = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Vatican museums";
            p.CityCenterDistance = 4;
            p.Rating = 0.0f;
            p.Latitude = 41.9065f;
            p.Longitude = 12.4536f;
            p.Description  = "The Vatican Museums (Italian: Musei Vaticani) are the museums of the Vatican City and are located within the citys boundaries. They display works from the immense collection built up by the Popes throughout the centuries including some of the most renowned classical sculptures and most important masterpieces of Renaissance art in the world. The museums contain roughly 70,000 works, of which 20,000 are on display, and currently employ 640 people who work in 40 different administrative, scholarly, and restoration departments. Pope Julius II founded the museums in the early 16th century. The Sistine Chapel, with its ceiling decorated by Michelangelo and the Stanze di Raffaello decorated by Raphael, are on the visitor route through the Vatican Museums. In 2013, they were visited by 6 million people, which combined makes it the 6th most visited art museum in the world.";
            DataProviderCreate.CreatePlace(p);
            int vatMuseums = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Forum Romanum";
            p.CityCenterDistance = 5;
            p.Latitude = 41.8925f;
            p.Longitude = 12.4853f;
            p.Description = "The Roman (Latin: Forum Romanum; Italian: Foro Romano) is a rectangular forum (plaza) surrounded by the ruins of several important ancient government buildings at the center of the city of Rome. Citizens of the ancient city referred to this space, originally a marketplace, as the Forum Magnum, or simply the Forum.It was for centuries the center of Roman public life: the site of triumphal processions and elections; the venue for public speeches, criminal trials, and gladiatorial matches; and the nucleus of commercial affairs.Here statues and monuments commemorated the citys great men. The teeming heart of ancient Rome, it has been called the most celebrated meeting place in the world, and in all history.[1] Located in the small valley between the Palatine and Capitoline Hills, the Forum today is a sprawling ruin of architectural fragments and intermittent archaeological excavations attracting 4.5 million sightseers yearly.[2]";
            DataProviderCreate.CreatePlace(p);
            int forumRomanum = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Pantheon";
            p.CityCenterDistance = 32;
            p.Latitude = 41.8986f;
            p.Longitude = 12.4769f;
            p.Description = "The Pantheon- from Greek Πάνθειον Pantheion meaning \"[temple] of every god\") is a former Roman temple, now a church, in Rome, Italy, on the site of an earlier temple commissioned by Marcus Agrippa during the reign of Augustus (27 BC – 14 AD). The present building was completed by the emperor Hadrian and probably dedicated about 126 AD. He retained Agrippas original inscription, which has confused its date of construction as the original Pantheon burnt down so it is not certain when the present one was built.[2]";
            DataProviderCreate.CreatePlace(p);
            int pantheonRome = Int32.Parse(DataProviderGet.GetMaxId("Place"));




            City c = new City();

            c.Name = "Niš";
            c.CenterLatitude = 43.320797f;
            c.CenterLongitude = 21.896105f;
            DataProviderCreate.CreateCity(c);
            int nis = Int32.Parse(DataProviderGet.GetMaxId("City"));


            c.Name = "Beograd";
            c.CenterLatitude = 44.793582f;
            c.CenterLongitude = 20.453716f;
            DataProviderCreate.CreateCity(c);
            int beograd = Int32.Parse(DataProviderGet.GetMaxId("City"));

            c.Name = "Kursumlija";
            c.CenterLatitude = 43.140667f;
            c.CenterLongitude = 21.273391f;
            DataProviderCreate.CreateCity(c);
            int kursumlija = Int32.Parse(DataProviderGet.GetMaxId("City"));


            c.Name = "Kragujevac";
            c.CenterLatitude = 44.013028f;
            c.CenterLongitude = 20.906487f;
            DataProviderCreate.CreateCity(c);
            int kragujevac = Int32.Parse(DataProviderGet.GetMaxId("City"));


            //italijanski gradovi
            c.Name = "Venice";
            c.CenterLatitude = 45.4408f;
            c.CenterLongitude = 12.3155f;
            DataProviderCreate.CreateCity(c);
            int venice = Int32.Parse(DataProviderGet.GetMaxId("City"));

            c.Name = "Rome";
            c.CenterLatitude = 41.908f;
            c.CenterLongitude = 12.4964f;
            DataProviderCreate.CreateCity(c);
            int rome = Int32.Parse(DataProviderGet.GetMaxId("City"));

            c.Name = "Milano";
            c.CenterLatitude = 45.4654f;
            c.CenterLongitude = 9.1859f;
            DataProviderCreate.CreateCity(c);
            int milan = Int32.Parse(DataProviderGet.GetMaxId("City"));


            Country ct = new Country();

            ct.Name = "Serbia";
            ct.OverallRating = 0.0f;
            ct.NationalFlag = "/Content/Images/rs.jpg";
            ct.PromotionalVideoURL = "https://www.youtube.com/embed/xDHpcAFSMr0";
            DataProviderCreate.CreateCountry(ct);
            int serbia = Int32.Parse(DataProviderGet.GetMaxId("Country"));

            //nove drzave 
            ct.Name = "Italy";
            ct.OverallRating = 0.0f;
            ct.NationalFlag = "/Content/Images/it.svg";
            ct.PromotionalVideoURL = "https://www.youtube.com/watch?v=TeVs1FeRDAw";
            DataProviderCreate.CreateCountry(ct);
            int italia = Int32.Parse(DataProviderGet.GetMaxId("Country"));


            InterestTag it = new InterestTag();
            it.Name = "historical";
            it.Type = "nemam pojma";
            it.FieldOfLife = "ko bi ga znao";
            DataProviderCreate.CreateInterestTag(it);
            int historical = Int32.Parse(DataProviderGet.GetMaxId("InterestTag"));

            it.Name = "religion";
            it.Type = "nemam pojma";
            it.FieldOfLife = "ko bi ga znao";
            DataProviderCreate.CreateInterestTag(it);
            int religion = Int32.Parse(DataProviderGet.GetMaxId("InterestTag"));

            it.Name = "nature";
            it.Type = "nemam pojma";
            it.FieldOfLife = "ko bi ga znao";
            DataProviderCreate.CreateInterestTag(it);
            int nature = Int32.Parse(DataProviderGet.GetMaxId("InterestTag"));

            //novi tagovi

            it.Name = "sports";
            it.Type = "sport events and teams";
            it.FieldOfLife = "sport";
            DataProviderCreate.CreateInterestTag(it);
            int sports = Int32.Parse(DataProviderGet.GetMaxId("InterestTag"));


            //Status s = new Status();
            //s.Description = "Ovo je obican korisnik!";
            //s.StatusName = "User";
            //DataProviderCreate.CreateStatus(s);
            //int user = Int32.Parse(DataProviderGet.GetMaxId("Status"));

            //s.Description = "Ovo je obican admin!";
            //s.StatusName = "Admin";
            //DataProviderCreate.CreateStatus(s);
            //int admin = Int32.Parse(DataProviderGet.GetMaxId("Status"));

            //s.Description = "Ovo je obican moderator!";
            //s.StatusName = "Moderator";
            //DataProviderCreate.CreateStatus(s);
            //int moderator = Int32.Parse(DataProviderGet.GetMaxId("Status"));

            // veze

            DataRelationships.Follow(mitar, pujo);
            DataRelationships.Follow(mitar, vucko);
            DataRelationships.Follow(vucko, voja);
            DataRelationships.Follow(voja, tonushleb);
            DataRelationships.Follow(pujo, perica);
            DataRelationships.Follow(perica, mitar);
            DataRelationships.Follow(mitar, stojan);
            DataRelationships.Follow(vucko, stojan);
            DataRelationships.Follow(voja, stojan);
            DataRelationships.Follow(pujo, stojan);
            DataRelationships.Follow(perica, stojan);

            DataRelationships.VisitedPlace(mitar, djavolja_varos, DateTime.Now);
            DataRelationships.VisitedPlace(pujo, sumarice, DateTime.Now);
            DataRelationships.VisitedPlace(voja, medijana, DateTime.Now);
            DataRelationships.VisitedPlace(stojan, cele_kula, DateTime.Now);
            DataRelationships.VisitedPlace(mitar, cele_kula, DateTime.Now);
            DataRelationships.VisitedPlace(voja, djavolja_varos, DateTime.Now);


            DataRelationships.PlansToVisit(mitar, medijana);
            DataRelationships.PlansToVisit(mitar, hramSCK);
            DataRelationships.PlansToVisit(voja, hramSCK);
            DataRelationships.PlansToVisit(perica, hramSCK);
            DataRelationships.PlansToVisit(pujo, hramSCK);

            DataRelationships.CurrentlyAtPlace(mitar, hramSCK);
            DataRelationships.CurrentlyAtPlace(pujo, cele_kula);

            DataRelationships.HasPlace(nis, medijana);
            DataRelationships.HasPlace(nis, cele_kula);
            DataRelationships.HasPlace(nis, hramSCK);
            DataRelationships.HasPlace(nis, bubanj);
            DataRelationships.HasPlace(kursumlija, djavolja_varos);
            DataRelationships.HasPlace(kragujevac, sumarice);

            //venecija
            DataRelationships.HasPlace(venice, sanMarcoBas);
            DataRelationships.HasPlace(venice, piazzaSM);

            //milano
            DataRelationships.HasPlace(milan, piazzaDD);
            
            //rim
            DataRelationships.HasPlace(rome, pantheonRome);
            DataRelationships.HasPlace(rome, colosseum);
            DataRelationships.HasPlace(rome, vatMuseums);
            DataRelationships.HasPlace(rome, forumRomanum);



            DataRelationships.HasCity(serbia, nis);
            DataRelationships.HasCity(serbia, beograd);
            DataRelationships.HasCity(serbia, kursumlija);
            DataRelationships.HasCity(serbia, kragujevac);

            //italija
            DataRelationships.HasCity(italia, venice);
            DataRelationships.HasCity(italia, rome);
            DataRelationships.HasCity(italia, milan);



            DataRelationships.Recommend(stojan, medijana, "Stojan ne razume ko mu je iz Engleske poslao paket.", 5);
            DataRelationships.Recommend(stojan, cele_kula, "Stojan ne razume ko mu je iz Engleske poslao paket.", 5);
            DataRelationships.Recommend(stojan, sumarice, "Stojan ne razume ko mu je iz Engleske poslao paket.", 5);
            DataRelationships.Recommend(mitar, medijana, "Mitar voz hir!", 6);
            DataRelationships.Recommend(pujo, medijana, "Pujan je lud!", 6);
            DataRelationships.Recommend(voja, medijana, "Moja ideovogija je besmvtna!", 10);
            DataRelationships.Recommend(mitar, hramSCK, "Boze uzmi u svoje nezne ruke ovaj avion i nezno ga spusti na aerodrom Muhare u Cikago!", 6);
            DataRelationships.Recommend(mitar, bubanj, "Boze uzmi u svoje nezne ruke ovaj avion i nezno ga spusti na aerodrom Muhare u Cikago!", 8);
            DataRelationships.Recommend(voja, djavolja_varos, "Ovaj \"gvad\" bi bio pvavo mesto za onu vesticu Kavlu del Ponte", 10);
            System.Threading.Thread.Sleep(1000);
            DataRelationships.Recommend(pujo, djavolja_varos, "Tromo se vreme vuče!", 9);
            System.Threading.Thread.Sleep(1000);
            DataRelationships.Recommend(mitar, djavolja_varos, "I ničeg novog nema,", 8);
            System.Threading.Thread.Sleep(1000);
            DataRelationships.Recommend(perica, djavolja_varos, "Danas sve ko juče", 7);
            System.Threading.Thread.Sleep(1000);
            DataRelationships.Recommend(vucko, djavolja_varos, "Sutra se isto sprema.", 6);


            DataRelationships.Recommend(mitar, sanMarcoBas, "haha", 3);
            DataRelationships.Recommend(mitar, colosseum, "hahaa", 4);
            DataRelationships.Recommend(mitar, forumRomanum, "hahaa", 5);
            DataRelationships.Recommend(mitar, pantheonRome, "hahaa", 6);
            DataRelationships.Recommend(mitar, piazzaDD, "hahaa", 7);
            DataRelationships.Recommend(mitar, piazzaSM, "hahaa", 8);
            DataRelationships.Recommend(mitar, vatMuseums, "hahaaaa", 9);

            DataRelationships.VisitedPlace(mitar, sanMarcoBas, DateTime.Now);
            DataRelationships.VisitedPlace(mitar, colosseum, DateTime.Now);
            DataRelationships.VisitedPlace(mitar, forumRomanum, DateTime.Now);
            DataRelationships.VisitedPlace(mitar, pantheonRome, DateTime.Now);
            DataRelationships.VisitedPlace(mitar, piazzaDD, DateTime.Now);
            DataRelationships.VisitedPlace(mitar, piazzaSM, DateTime.Now);
            DataRelationships.VisitedPlace(mitar, vatMuseums, DateTime.Now);

            //DataRelationships.HasStatus(mitar, "User");
            //DataRelationships.HasStatus(stojan, "Admin");
            //DataRelationships.HasStatus(voja, "Moderator");
            //DataRelationships.HasStatus(perica, "User");
            //DataRelationships.HasStatus(pujo, "User");
            //DataRelationships.HasStatus(tonushleb, "User");

            DataRelationships.HasInterest(mitar, "historical");
            DataRelationships.HasInterest(stojan, "religion");
            DataRelationships.HasInterest(mitar, "religion");
            DataRelationships.HasInterest(voja, "historical");
            DataRelationships.HasInterest(perica, "historical");
            DataRelationships.HasInterest(tonushleb, "historical");
            DataRelationships.HasInterest(tonushleb, "religion");

            DataRelationships.HasInterestTag(cele_kula, "historical");
            DataRelationships.HasInterestTag(hramSCK, "historical");
            DataRelationships.HasInterestTag(hramSCK, "religion");
            DataRelationships.HasInterestTag(sumarice, "historical");
            DataRelationships.HasInterestTag(djavolja_varos, "nature");
            DataRelationships.HasInterestTag(medijana, "historical");
            DataRelationships.HasInterestTag(bubanj, "historical");


            //italijanska mesta
            DataRelationships.HasInterestTag(piazzaDD, "historical");
            DataRelationships.HasInterestTag(piazzaSM, "historical");
            DataRelationships.HasInterestTag(forumRomanum, "historical");
            DataRelationships.HasInterestTag(colosseum, "historical");
            DataRelationships.HasInterestTag(sanMarcoBas, "historical");
            DataRelationships.HasInterestTag(pantheonRome, "historical");
            DataRelationships.HasInterestTag(vatMuseums, "historical");

            DataRelationships.HasInterestTag(vatMuseums, "religion");
            DataRelationships.HasInterestTag(sanMarcoBas, "religion");
            DataRelationships.HasInterestTag(piazzaSM, "religion");



            this.CreatePlacePictureList("Bubanj", 4, bubanj);
            this.CreatePlacePictureList("CeleKula", 4, cele_kula);
            this.CreatePlacePictureList("SCK", 4, hramSCK);
            this.CreatePlacePictureList("Medijana", 4, medijana);
            this.CreatePlacePictureList("Sumarice", 4, sumarice);
            this.CreatePlacePictureList("DjavoljaVaros", 4, djavolja_varos);

            this.CreatePlacePictureList("BasilicaSanMarco", 4, sanMarcoBas);
            this.CreatePlacePictureList("Colosseum", 4, colosseum);
            this.CreatePlacePictureList("ForumRomanum", 5, forumRomanum);
            this.CreatePlacePictureList("Pantheon", 3, pantheonRome);
            this.CreatePlacePictureList("PiazaDuomo", 4, piazzaDD);
            this.CreatePlacePictureList("PiazzaSanMarco", 4, piazzaSM);
            this.CreatePlacePictureList("VaticanMuseums", 4, vatMuseums);

         



            DataProviderUpdate.UpdatePlaceRating(medijana);
            DataProviderUpdate.UpdatePlaceRating(bubanj);
            DataProviderUpdate.UpdatePlaceRating(djavolja_varos);
            DataProviderUpdate.UpdatePlaceRating(sumarice);
            DataProviderUpdate.UpdatePlaceRating(cele_kula);
            DataProviderUpdate.UpdatePlaceRating(hramSCK);

            DataProviderUpdate.UpdatePlaceRating(piazzaSM);
            DataProviderUpdate.UpdatePlaceRating(piazzaDD);
            DataProviderUpdate.UpdatePlaceRating(pantheonRome);
            DataProviderUpdate.UpdatePlaceRating(vatMuseums);
            DataProviderUpdate.UpdatePlaceRating(forumRomanum);
            DataProviderUpdate.UpdatePlaceRating(sanMarcoBas);
            DataProviderUpdate.UpdatePlaceRating(colosseum);



            System.Threading.Thread.Sleep(50);

            DataProviderUpdate.UpdateCountryRating(serbia);
            DataProviderUpdate.UpdateCountryRating(italia);

        }

        private void add_place_pic_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string f_name = Path.GetFileName(ofd.FileName);

                string path = "/Content/Images/" + f_name;      // ovo vidi kako ti vec treba

                int id = 0;
                if (Int32.TryParse(this.placeId.Text, out id))
                    DataProviderUpdate.AddPictureOfPlace(path, id);
            }
        }

        public void CreatePlacePictureList(string placeFolderName, int number, int placeId)  // imena slika u folderu mora da krece od 1
        {
          
            for(int i = 1; i < number + 1; i++)
            {
                DataProviderUpdate.AddPictureOfPlace("/Content/Images/" + placeFolderName + "/" + i + ".jpg", placeId);
            }
        }

        private void tb_1_Click(object sender, EventArgs e)
        {
            List<Recommendation> rl = DataProviderGet.GetPlaceRecommendationsByTime(1, false);

            string temp = rl[0].RecommendationTime.ToString();
            //temp = temp.Remove(temp.Length - 2);
            string s  = string.Empty;

            foreach (Recommendation r in rl)
                s += r.RecommendationTime.ToString() + "\n";

            MessageBox.Show(s);
           
        }

        private void aditd_1_Click(object sender, EventArgs e)
        {
            int messId = DataProviderCreate.CreateMessage("Kako je danas Vojislave?.", "Dobra kao dobar dan!");
            DataRelationships.SendMessage(2, 6, 2);

            int messId2 = DataProviderCreate.CreateMessage("Ti nemas srce, nemas dusu.", "Klington");
            DataRelationships.SendMessage(1, 6, 2);

            int messId3 = DataProviderCreate.CreateMessage("Klinton nije sluzio vojsku.", "Suntavilo");
            DataRelationships.SendMessage(3, 2, messId3);

            int messId4 = DataProviderCreate.CreateMessage("Vojo, nemoj tako.", "Rakijestina");
            DataRelationships.SendMessage(2, 6, messId4);

            int messId5 = DataProviderCreate.CreateMessage("Ok?", "Ok.");
            DataRelationships.SendMessageToUser("Mitar", "Pujo", messId5);


            List<DomainModel.Message> mitrovePoruke = DataProviderGet.GetAllMessagesSentOrReceivedByUser(2, "SENT");

            foreach(var m  in mitrovePoruke)
            {
                MessageBox.Show(m.Text);
            }

            List<DomainModel.Message> mitrovePrimljene = DataProviderGet.GetAllMessagesSentOrReceivedByUser(2, "RECEIVED");
            foreach(var m in mitrovePrimljene)
            {
                MessageBox.Show(m.Text);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Country> t = DataProviderGet.GetTopNVisitedCountries(10);
            foreach (Country p in t)
                MessageBox.Show(p.Name);
        }
    }
}
