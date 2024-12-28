string?[] elements =
[
    "H","He",
    "Li","Be","B","C","N","O","F","Ne",
    "Na","Mg","Al","Si","P","S","Cl","Ar",
    "K","Ca","Sc","Ti","V","Cr","Mn","Fe","Co","Ni","Cu","Zn","Ga","Ge","As","Se","Br","Kr",
    "Rb","Sr","Y","Zr","Nb","Mo","Tc","Ru","Rh","Pd","Ag","Cd","In","Sn","Sb","Te","I","Xe",
    "Cs","Ba","La","Ce","Pr","Nd","Pm","Sm","Eu","Gd","Tb","Dy","Ho","Er","Tm","Yb",
    "Lu","Hf","Ta","W","Re","Os","Ir","Pt","Au","Hg","Tl","Pb","Bi","Po","At","Rn",
    "Fr","Ra","Ac","Th","Pa","U","Np","Pu","Am","Cm","Bk","Cf","Es","Fm","Md","No",
    "Lr","Rf","Db","Sg","Bh","Hs","Mt","Ds","Rg","Cn","Nh","Fl","Mc","Lv","Ts","Og"
];

string[] elementNames =
[
    "Hydrogen", "Helium",
    "Lithium", "Beryllium", "Boron", "Carbon", "Nitrogen", "Oxygen", "Fluorine", "Neon",
    "Sodium", "Magnesium", "Aluminum", "Silicon", "Phosphorus", "Sulfur", "Chlorine", "Argon",
    "Potassium", "Calcium", "Scandium", "Titanium", "Vanadium", "Chromium", "Manganese", "Iron", "Cobalt",
    "Nickel", "Copper", "Zinc", "Gallium", "Germanium", "Arsenic", "Selenium", "Bromine", "Krypton",
    "Rubidium", "Strontium", "Yttrium", "Zirconium", "Niobium", "Molybdenum", "Technetium", "Ruthenium", "Rhodium",
    "Palladium", "Silver", "Cadmium", "Indium", "Tin", "Antimony", "Tellurium", "Iodine", "Xenon",
    "Cesium", "Barium", "Lanthanum", "Cerium", "Praseodymium", "Neodymium", "Promethium", "Samarium", "Europium",
    "Gadolinium", "Terbium", "Dysprosium", "Holmium", "Erbium", "Thulium", "Ytterbium", "Lutetium", "Hafnium",
    "Tantalum", "Tungsten", "Rhenium", "Osmium", "Iridium", "Platinum", "Gold", "Mercury", "Thallium",
    "Lead", "Bismuth", "Polonium", "Astatine", "Radon", "Francium", "Radium", "Actinium", "Thorium",
    "Protactinium", "Uranium", "Neptunium", "Plutonium", "Americium", "Curium", "Berkelium", "Californium",
    "Einsteinium", "Fermium", "Mendelevium", "Nobelium", "Lawrencium", "Rutherfordium", "Dubnium", "Seaborgium", 
    "Bohrium", "Hassium", "Meitnerium", "Darmstadtium ", "Roentgenium ", "Copernicium ", "Nihonium", "Flerovium", 
    "Moscovium", "Livermorium", "Tennessine", "Oganesson",
];
string[] orbitals = 
    ["1s", "2s", "2p", "3s", "3p", "4s", "3d", "4p", "5s", "4d", "5p", "6s", "4f", "5d","6p", "7s", "5f", "6d", "7p"];

var shellSizes = new SortedDictionary<string, int>
{
    { "s", 2 }, { "p", 6 }, { "d", 10 }, { "f", 14 }
};
Start();
void Start(){
    string? elementSym = TakeInput();

    if(elementSym is "Exit" or "exit")
    {
        Environment.Exit(0);
    }
    if(elementSym != null && elementSym.Length > 2)
    {
        elementSym = elements[Array.IndexOf(elementNames, elementSym)];
    }
    var elementAtomicNumber = Array.IndexOf(elements, elementSym) + 1;
    if(elementAtomicNumber == 0)
    {
        Console.WriteLine("You ran the program with an invalid argument.");
        Console.WriteLine("Please enter a valid atomic symbol or name like \"Zinc\"or \"Al\".");
        Start();
    }
    Console.WriteLine(
        $"Madelung's rule states {elementNames[elementAtomicNumber-1]}'s electron configuration is {MakeElectronConfig(elementAtomicNumber)}");
    Console.WriteLine("Hit enter to exit the program or r to re-run the program.");
    var userInput = Console.ReadLine();
    if(userInput == "r"){
        Start();
    }
}


string? TakeInput()
{
    var input = ""; 
    while (input != null && Array.IndexOf(elements, input) == -1 && Array.IndexOf(elementNames, input) == -1 && input != "exit" && input != "Exit")
    {
        Console.WriteLine("Enter the word exit if you want to quit the program.");
        Console.WriteLine(
            "Which element symbol or name(i.e. \"Francium\" or \"Si\") do you want the name of?");
        input = Console.ReadLine();
    }
    return input;  
}

string MakeElectronConfig(int electronsRemaining)
{
    var electronConfigOutput = "";
    var currentOrbital = 0;
    var electronsLeftInOrbital = shellSizes[orbitals[currentOrbital].Substring(1)];
    var electronsToAppend = 0;
    while (electronsRemaining >= 0)
    {
        electronsLeftInOrbital -= 1;
        electronsRemaining -= 1;
        electronsToAppend += 1;
        if (electronsLeftInOrbital == 0||electronsRemaining == 0)
        {
            electronConfigOutput = string.Concat(electronConfigOutput,
                ComposeOrbital(orbitals[currentOrbital], electronsToAppend));
            if (currentOrbital != orbitals.Length-1)
            {
                        currentOrbital += 1;
            }
            electronsLeftInOrbital = shellSizes[orbitals[currentOrbital].Substring(1)];
            electronsToAppend = 0;
        }
    }
    return electronConfigOutput;
}

string ComposeOrbital(string orbitalName, int electronCount)
{
    return string.Concat(orbitalName, "^", electronCount.ToString(), " "); 
}