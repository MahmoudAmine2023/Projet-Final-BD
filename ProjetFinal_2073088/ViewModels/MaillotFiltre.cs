using ProjetFinal_2073088.Models;

namespace ProjetFinal_2073088.ViewModels
{
    public class MaillotFiltre
    {
        public List<string> AllTeams { get; set; }
        public List<int> AllYears { get; set; }
        public List<string> SelectedTeams { get; set; }
        public List<int> SelectedYears { get; set; }
        public List<Maillot> Maillots { get; set; }
    }
}
