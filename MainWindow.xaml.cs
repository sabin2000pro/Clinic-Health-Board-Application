using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLayer;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HealthFacade healthSystem = new HealthFacade();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = "";
            if (!healthSystem.addStaff(1, "James", "Henry", "21 Accia Road", "Edinburgh", "General Practitioner", 55.932221, -3.214164))
                txtOutput.Text += " Staff member not added \n";

            if (!healthSystem.addStaff(2, "Sally", "Wilson", "21 Accia Road", "Edinburgh", "General Practitioner", 55.932221, -3.214164))
                txtOutput.Text += " Staff member not added \n";

            if (!healthSystem.addStaff(3, "Roland", "Smith", "21 Accia Road", "Edinburgh", "Social Worker", 55.932221, -3.214164))
                txtOutput.Text += " Staff member not added \n";

            if (!healthSystem.addStaff(4, "Mary", "Jones", "21 Accia Road", "Edinburgh", "Social Worker", 55.932221, -3.214164))
                txtOutput.Text += " Staff member not added \n";

            if(!healthSystem.addStaff(5, "Phillip", "Jones", "21 Accia Road", "Edinburgh", "Community Nurse", 55.932221, -3.214164))
                txtOutput.Text += " Staff member not added \n";

            if (!healthSystem.addStaff(6, "Martha", "Rigg", "21 Accia Road", "Edinburgh", "Community Nurse", 55.932221, -3.214164))
                txtOutput.Text += " Staff member not added \n";

            if (!healthSystem.addStaff(7, "Mike", "Heathcoat", "21 Accia Road", "Edinburgh", "Care Worker", 55.932221, -3.214164))
                txtOutput.Text += " Staff member not added \n";

            if (!healthSystem.addStaff(8, "Jo", "Shaw", "21 Accia Road", "Edinburgh", "Care Worker", 55.932221, -3.214164))
                txtOutput.Text += " Staff member not added \n";

            txtOutput.Text += healthSystem.getStaffList() +"\n";
        }
        private void btnAddClients_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = "";
            if (!healthSystem.addClient(1, "Claire", "Wentworth", "1 Low Rd", "Edinburgh", 55.937894, -3.194088))
                txtOutput.Text += " Client member not added \n";
            if (!healthSystem.addClient(2, "Douglas", "Brown", "2 Mid Rd", "Edinburgh", 55.932317, -3.192716))
                txtOutput.Text += " Client member not added \n";
            if (!healthSystem.addClient(3, "Jimmy", "Green", "31 High Rd", "Edinburgh", 55.942605, -3.180533))
                txtOutput.Text += " Client member not added \n";

            txtOutput.Text += healthSystem.getClientList() + "\n";
        }

        private void BtnAddVisit_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = "";
            try
            { 
                if (healthSystem.addVisit(new int[2] { 1, 3 }, 1, visitTypes.assessment, "01/01/2020 09:00")) //Should be OK
                    txtOutput.Text += "Visit 1 added.";
            }
            catch (Exception ex)
            {
                txtOutput.Text += ex.Message;
            }

            try { 
            if (healthSystem.addVisit(new int[2] { 1, 3 }, 1, 999, "01/01/2020 09:00")) //Should fail as type not valid
                    txtOutput.Text += "Visit 2 added.";
            }
            catch (Exception ex)
            {
                txtOutput.Text += ex.Message;
            }
            try { 
            if (healthSystem.addVisit(new int[2] {7,8}, 2, visitTypes.bath, "01/01/2020 18:00")) //Should be OK
                    txtOutput.Text += "Visit 3 added.";
            }
            catch (Exception ex)
            {
                txtOutput.Text += ex.Message;
            }
            try { 
            if (healthSystem.addVisit(new int[1] { 1}, 1, visitTypes.meal, "01/01/2020 09:00")) //Should fail as wrong staff type
                    txtOutput.Text += "Visit 4 added.";
            }
            catch (Exception ex)
            {
                txtOutput.Text += ex.Message;
            }
            try { 
            if (healthSystem.addVisit(new int[1] { 5 }, 4, visitTypes.medication, "01/01/2020 10:00")) //Should fail as client does not exist
                    txtOutput.Text += "Visit 5 added.";
        }
            catch (Exception ex)
            {
                txtOutput.Text += ex.Message;
            }
            try { 
                if (healthSystem.addVisit(new int[2] { 1, 3 }, 2, visitTypes.assessment, "01/01/2020 09:00")) //Should fail as staff clash with visit 1
                    txtOutput.Text += "Visit 6 added.";
            }
            catch (Exception ex)
            {
                txtOutput.Text += ex.Message;
            }
            txtOutput.Text += healthSystem.getVisitList();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            healthSystem.clear();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            healthSystem.clear();
            healthSystem.load();
            txtOutput.Text += healthSystem.getVisitList();
            txtOutput.Text += healthSystem.getClientList();
            txtOutput.Text += healthSystem.getStaffList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            healthSystem.save();
        }
    }
}