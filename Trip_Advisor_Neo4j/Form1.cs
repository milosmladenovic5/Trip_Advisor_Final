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

namespace Trip_Advisor_Neo4j
{
    public partial class Form1 : Form
    {
        public Form1()
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
            p.Description = "Đavolja Varoš je vrlo atraktivan prirodni fenomen koji se nalazi na jugu Srbije, oko 90 km jugozapadno od Niša. Čine je 202 kamene figure koje su nastale dugotrajnim i strpljivim radom prirode";
            DataProviderCreate.CreatePlace(p);
            int djavolja_varos = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Spomen-park „Kragujevački oktobar“";
            p.CityCenterDistance = 14;
            //p.Pictures = this.CreatePlacePictureList("Sumarice", 4);
            p.Rating = 0.0f;
            p.Description = "Spomen-park „Kragujevački oktobar“, predstavlja spomen kompleks podignut u znak sećanja na nedužne žrtve Kragujevačkog masakra koji su počinili pripadnici Vermahta 21. oktobra 1941. godine. Tog dana nemački okupatori su u Šumaricama streljali oko 3.000 stanovnika Kragujevca i okolnih mesta, a među njima je bilo i 300 učenika kragujevačkih srednjih škola i šegrta koji su već radili, kao i 15 dečaka, čistača obuće, od 12 do 15 godina starosti.";
            DataProviderCreate.CreatePlace(p);
            int sumarice = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Ćele-kula";
            //p.Pictures = this.CreatePlacePictureList("CeleKula", 4);
            p.CityCenterDistance = 4;
            p.Rating = 0.0f;
            p.Description = "Ćele-kula (tur. kelle kulesi, \"kula od lobanja\") je spomenik iz Prvog srpskog ustanka koji je u znak odmazde tadašnja Turska vlast u Srbiji izgradila od lobanja, poginulih srpskih ratnika, predvođenih Stevanom Sinđelićem, u bici na Čegru. Nalazi se na 4 km od centra Niša, na putu ka Niškoj Banji. Svrstana je u spomenike kulture od izuzetnog značaja za Republiku Srbiju i danas predstavlja muzejski objekat.";
            DataProviderCreate.CreatePlace(p);
            int cele_kula = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Medijana";
            //p.Pictures = this.CreatePlacePictureList("Medijana", 4);
            p.CityCenterDistance = 12;
            p.Rating = 0.0f;
            p.Description = "Medijana, rimska Medijana (lat. Mediana) danas arheološki park u istočnom delu Niša, na pola puta za Nišku Banju, u vreme vladavine cara Konstantina Velikog, rimsko naselje ili kompleks letnjih rezidencija tipa urbanih vila i veliko poljoprivredno gazdinstvo, otvorenog tipa, pokraj puta, koji je od Naisa vodio ka istoku, prema Serdici i dalje prema Konstantinopolisu. Naselje je nastalo na samom kraju 3. ili početkom 4. veka.";
            DataProviderCreate.CreatePlace(p);
            int medijana = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Hram Svetog cara Konstantina i carice Jelene";
            //p.Pictures = this.CreatePlacePictureList("HKC", 4);
            p.CityCenterDistance = 13;
            p.Rating = 0.0f;
            p.Description = "Hram Svetog cara Konstantina i carice Jelene je hram Srpske pravoslavne crkve koji se nalazi u novom delu grada Niša, a koji je grad Niš posvetio znamenitom Nišliji Konstantinu Velikom i njegovoj majci Jeleni.Hram se nalazi u jednom od najvećih niških parkova, parku Svetog Save. Projektant hrama je Jovan Mandić. Hram je izgrađen u vizantijskom stilu sa dodatkom dva zvonika.";
            DataProviderCreate.CreatePlace(p);
            int hramSCK = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            p.Name = "Spomen-park Bubanj";
            //p.Pictures = this.CreatePlacePictureList("Bubanj", 4);
            p.CityCenterDistance = 19;
            p.Rating = 0.0f;
            p.Description = "Spomen park Bubanj predstavlja spomen kompleks, sagrađen u sećanje na streljane građane Niša i južne Srbije u Drugom svetskom ratu i nalazi se jugozapadno od Niša, u niškoj opštini Palilula.[1] Spomen park Bubanj je kao autentično mesto masovnog fašističkog terora, stavljen pod zaštitu države u maju 1973 godine. Dok je aprila 1979. godine odlukom Skupštine Srbije spomen park Bubanj proglašen kulturnim dobrom od izuzetnog značaja";
            DataProviderCreate.CreatePlace(p);
            int bubanj = Int32.Parse(DataProviderGet.GetMaxId("Place"));

            City c = new City();

            c.Name = "Niš";
            DataProviderCreate.CreateCity(c);
            int nis = Int32.Parse(DataProviderGet.GetMaxId("City"));


            c.Name = "Beograd";
            DataProviderCreate.CreateCity(c);
            int beograd = Int32.Parse(DataProviderGet.GetMaxId("City"));

            c.Name = "Kursumlija";
            DataProviderCreate.CreateCity(c);
            int kursumlija = Int32.Parse(DataProviderGet.GetMaxId("City"));


            c.Name = "Kragujevac";
            DataProviderCreate.CreateCity(c);
            int kragujevac = Int32.Parse(DataProviderGet.GetMaxId("City"));

            Country ct = new Country();
            ct.Name = "Serbia";
            ct.OverallRating = 0.0f;
            ct.NationalFlag = "/Content/Images/rs.jpg";
            DataProviderCreate.CreateCountry(ct);
            int serbia = Int32.Parse(DataProviderGet.GetMaxId("Country"));

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

            DataRelationships.HasCity(serbia, nis);
            DataRelationships.HasCity(serbia, beograd);
            DataRelationships.HasCity(serbia, kursumlija);
            DataRelationships.HasCity(serbia, kragujevac);

            DataRelationships.Recommend(stojan, medijana, "Stojan ne razume ko mu je iz Engleske poslao paket.", 5);
            DataRelationships.Recommend(stojan, cele_kula, "Stojan ne razume ko mu je iz Engleske poslao paket.", 5);
            DataRelationships.Recommend(stojan, sumarice, "Stojan ne razume ko mu je iz Engleske poslao paket.", 5);
            DataRelationships.Recommend(mitar, medijana, "Mitar voz hir!", 6);
            DataRelationships.Recommend(pujo, medijana, "Pujan je lud!", 6);
            DataRelationships.Recommend(voja, medijana, "Moja ideovogija je besmvtna!", 10);
            DataRelationships.Recommend(mitar, hramSCK, "Boze uzmi u svoje nezne ruke ovaj avion i nezno ga spusti na aerodrom Muhare u Cikago!", 6);
            DataRelationships.Recommend(mitar, bubanj, "Boze uzmi u svoje nezne ruke ovaj avion i nezno ga spusti na aerodrom Muhare u Cikago!", 8);
            DataRelationships.Recommend(voja, djavolja_varos, "Ovaj \"gvad\" bi bio pvavo mesto za onu vesticu Kavlu del Ponte", 10);
            DataRelationships.Recommend(pujo, djavolja_varos, "Tromo se vreme vuče!", 9);
            DataRelationships.Recommend(mitar, djavolja_varos, "I ničeg novog nema,", 8);
            DataRelationships.Recommend(perica, djavolja_varos, "Danas sve ko juče", 7);
            DataRelationships.Recommend(vucko, djavolja_varos, "Sutra se isto sprema.", 6);

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


            this.CreatePlacePictureList("Bubanj", 4, bubanj);
            this.CreatePlacePictureList("CeleKula", 4, cele_kula);
            this.CreatePlacePictureList("SCK", 4, hramSCK);
            this.CreatePlacePictureList("Medijana", 4, medijana);
            this.CreatePlacePictureList("Sumarice", 4, sumarice);
            this.CreatePlacePictureList("DjavoljaVaros", 4, djavolja_varos);


            DataProviderUpdate.UpdatePlaceRating(medijana);
            DataProviderUpdate.UpdatePlaceRating(bubanj);
            DataProviderUpdate.UpdatePlaceRating(djavolja_varos);
            DataProviderUpdate.UpdatePlaceRating(sumarice);
            DataProviderUpdate.UpdatePlaceRating(cele_kula);
            DataProviderUpdate.UpdatePlaceRating(hramSCK);

            System.Threading.Thread.Sleep(50);

            DataProviderUpdate.UpdateCountryRating(serbia);

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
            DateTime date = DateTime.Now;
            long n = long.Parse(date.ToString("yyyyMMddHHmmss"));
            long n1 = long.Parse(date.ToString("yyyyMMddHHmmss"));
        }
    }
}
