using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;

namespace MonopolySandbox
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// The different property color groups.
        /// Includes values for the following non-color properties as well:
        /// Corner
        /// Card
        /// Tax
        /// Utility
        /// Railroad
        /// </summary>
        public enum ColorGroups
        {
            Corner,
            Card,
            Tax,
            Utility,
            RailRoad,
            Brown,
            LightBlue,
            Purple,
            Orange,
            Red,
            Yellow,
            Green,
            Blue
        } // enum ColorGroups

        /// <summary>
        /// The different board piece symbols available.
        /// </summary>
        public enum PlayerSymbols
        {
            BANK,
            TopHat,
            Thimble,
            Iron,
            Boot,
            Battleship,
            Cannon,
            Racecar,
            Purse,
            RockingHorse,
            Lantern,
            ScottieDog,
            Wheelbarrow,
            HorseAndRider,
            SackOfMoney,
            Cat
        } // enum PlayerSymbols

        /// <summary>
        /// An object representing a space on the Monopoly board.
        /// </summary>
        public record BoardSpace
        {
            /// <summary>
            /// The position index of the space. 
            /// Go = 0, Boardwalk = 39
            /// </summary>
            public int BoardPosition { get; }
            /// <summary>
            /// The name of the board space.
            /// </summary>
            public string SpaceName { get; }
            /// <summary>
            /// The color value of the space or property.
            /// </summary>
            public ColorGroups PropertyColor { get; }
            /// <summary>
            /// The name of the property's current owner.
            /// </summary>
            public string OwnerName { get; set; }
            /// <summary>
            /// The price to buy the property (if it can be purchased).
            /// </summary>
            public int BuyPrice { get; }
            /// <summary>
            /// The price to build a house on a property (if available).
            /// </summary>
            public int BuildingPrice { get; }
            /// <summary>
            /// The rent due if zero houses are present.
            /// </summary>
            public int ZeroHouseRent { get; }
            /// <summary>
            /// The rent due if one house is present.
            /// </summary>
            public int OneHouseRent { get; }
            /// <summary>
            /// The rent due if two houses are present.
            /// </summary>
            public int TwoHouseRent { get; }
            /// <summary>
            /// The rent due if three houses are present.
            /// </summary>
            public int ThreeHouseRent { get; }
            /// <summary>
            /// The rent due if four houses are present.
            /// </summary>
            public int FourHouseRent { get; }
            /// <summary>
            /// The rent due if a hotel (five houses) is present.
            /// </summary>
            public int HotelRent { get; }
            /// <summary>
            /// The amount received by the player when the property is mortgaged.
            /// The ummortgage cost if 110% of the mortgage price, due to the bank.
            /// </summary>
            public int Mortgage { get; }
            /// <summary>
            /// Returns TRUE if the property has been mortgaged, FALSE if not.
            /// </summary>
            public bool IsMortgaged { get; set; }
            /// <summary>
            /// The current number of houses on the property (0 to 5)
            /// </summary>
            public int NumberOfHouses { get; set; }

            /// <summary>
            /// An object representing a space on the Monopoly board.
            /// </summary>
            /// <param name="boardPosition">The position index of the space. Go = 0, Boardwalk = 39</param>
            /// <param name="spaceName">The name of the board space.</param>
            /// <param name="propertyColor">The color value of the space or property.</param>
            /// <param name="ownerName">The name of the property's current owner.</param>
            /// <param name="buyPrice">The price to buy the property (if it can be purchased).</param>
            /// <param name="buildingPrice">The price to build a house on a property (if available).</param>
            /// <param name="zeroHouseRent">The rent due if zero houses are present.</param>
            /// <param name="oneHouseRent">The rent due if one house is present.</param>
            /// <param name="twoHouseRent">The rent due if two houses are present.</param>
            /// <param name="threeHouseRent">The rent due if three houses are present.</param>
            /// <param name="fourHouseRent">The rent due if four houses are present.</param>
            /// <param name="hotelRent">The rent due if a hotel (five houses) is present.</param>
            /// <param name="mortgage">The amount received by the player when the property is mortgaged. The ummortgage cost if 110% of the mortgage price, due to the bank.</param>
            /// <param name="isMortgaged">Returns TRUE if the property has been mortgaged, FALSE if not.</param>            /// 
            public BoardSpace(int boardPosition, string spaceName, ColorGroups propertyColor, string ownerName, int buyPrice, int buildingPrice, int zeroHouseRent, int oneHouseRent, int twoHouseRent, int threeHouseRent, int fourHouseRent, int hotelRent, int mortgage, bool isMortgaged)
            {
                BoardPosition = boardPosition;
                SpaceName = spaceName;
                PropertyColor = propertyColor;
                OwnerName = ownerName;
                BuyPrice = buyPrice;
                BuildingPrice = buildingPrice;
                ZeroHouseRent = zeroHouseRent;
                OneHouseRent = oneHouseRent;
                TwoHouseRent = twoHouseRent;
                ThreeHouseRent = threeHouseRent;
                FourHouseRent = fourHouseRent;
                HotelRent = hotelRent;
                Mortgage = mortgage;
                IsMortgaged = isMortgaged;
                NumberOfHouses = 0;
            } // BoardSpace
        }

        /// <summary>
        /// The collection of all the Board Spaces on the Board.
        /// </summary>
        List<BoardSpace> boardSpaces = new List<BoardSpace>();

        /// <summary>
        /// Represents a Player playing Monopoly.
        /// </summary>
        public record Player
        {
            /// <summary>
            /// The name of the player.
            /// </summary>
            public string PlayerName { get; }
            /// <summary>
            /// The player's chosen symbol.
            /// </summary>
            public PlayerSymbols Symbol { get; }
            /// <summary>
            /// The player's account balance.
            /// </summary>
            public int Balance { get; set; }
            /// <summary>
            /// The player's current position on the board.
            /// </summary>
            public int BoardPosition { get; set; }

            /// <summary>
            /// Represents a Player playing Monopoly.
            /// </summary>
            /// <param name="playerName">The name of the player.</param>
            /// <param name="symbol">The player's chosen symbol.</param>
            /// <param name="balance">The player's account balance.</param>
            /// <param name="boardPosition">The player's current position on the board.</param>
            public Player(string playerName, PlayerSymbols symbol, int balance, int boardPosition)
            { 
                PlayerName = playerName;
                Symbol = symbol;
                Balance = balance;
                BoardPosition = boardPosition;
            } // Player

        } // Player
        
        /// <summary>
        /// Random number, used for dice rolls.
        /// </summary>
        Random rnd = new Random();

        /// <summary>
        /// The collection of all players playing the game.
        /// </summary>
        List<Player> players = new List<Player>();


        /// <summary>
        /// An object representing a Chance card
        /// </summary>
        public record ChanceCard
        {
            /// <summary>
            /// The text of the card
            /// </summary>
            public string CardText { get; }
            
            /// <summary>
            /// The board position to advance to if this card is drawn
            /// If the card does not require the player to move, this value will be -1
            /// </summary>
            public int AdvanceTo { get; }
           
            /// <summary>
            /// The number of space to move forward or back
            /// Used when a card instructs a player to move forward to back, rather than to a specific board space
            /// </summary>
            public int MoveForwardSpaces { get; }
            
            /// <summary>
            /// If the card results in the player collecting money, the amount to be collected will be stored here
            /// </summary>
            public int CollectPayment { get; }
            
            /// <summary>
            /// If the card results in the player making a payment, the payment amount will be recorded here
            /// </summary>
            public int MakePayment { get; }
            
            /// <summary>
            /// TRUE if the card requires the player to pay double rent to the railroad owner
            /// FALSE if not
            /// </summary>
            public bool RailRoadDoubleRent { get; }
            
            /// <summary>
            /// TRUE if the card requires the player to pay the Utility owner 10x dice roll
            /// FALSE if not
            /// </summary>
            public bool Utility10xDiceRoll { get; }
            
            /// <summary>
            /// TRUE if the card is a Get Out of Jail Free card
            /// FALSE if not
            /// </summary>
            public bool IsGetOutOfJailFree { get; }
            
            /// <summary>
            /// TRUE if the card requires the player to make building repairs
            /// FALSE if not
            /// </summary>
            public bool MakeBuildingRepairs { get; }
            
            /// <summary>
            /// TRUE if the card requires the player to make a payment to all other players
            /// FALSE if not
            /// </summary>
            public bool MakePaymentToEachPlayer { get; }

            /// <summary>
            /// An object representing a Chance card
            /// </summary>
            /// <param name="cardText">The text of the card</param>
            /// <param name="advanceTo">The board position to advance to if this card is drawn. If the card does not require the player to move, this value will be -1</param>
            /// <param name="moveForwardSpaces">The number of space to move forward or back. Used when a card instructs a player to move forward to back, rather than to a specific board space</param>
            /// <param name="collectPayment">If the card results in the player collecting money, the amount to be collected will be stored here</param>
            /// <param name="makePayment">If the card results in the player making a payment, the payment amount will be recorded here</param>
            /// <param name="railRoadDoubleRent">TRUE if the card requires the player to pay double rent to the railroad owner. FALSE if not</param>
            /// <param name="utility10xDiceRoll">TRUE if the card requires the player to pay the Utility owner 10x dice roll. FALSE if not</param>
            /// <param name="isGetOutOfJailFree">TRUE if the card requires the player to make building repairs. FALSE if not</param>
            /// <param name="makeBuildingRepairs">TRUE if the card requires the player to make building repairs. FALSE if not</param>
            /// <param name="makePaymentToEachPlayer">TRUE if the card requires the player to make a payment to all other players. FALSE if not</param>
            public ChanceCard(string cardText, int advanceTo, int moveForwardSpaces, int collectPayment, int makePayment, bool railRoadDoubleRent, bool utility10xDiceRoll, bool isGetOutOfJailFree, bool makeBuildingRepairs, bool makePaymentToEachPlayer)
            {
                CardText = cardText;
                AdvanceTo = advanceTo;
                MoveForwardSpaces = moveForwardSpaces;
                CollectPayment = collectPayment;
                MakePayment = makePayment;
                RailRoadDoubleRent = railRoadDoubleRent;
                Utility10xDiceRoll = utility10xDiceRoll;
                IsGetOutOfJailFree = isGetOutOfJailFree;
                MakeBuildingRepairs = makeBuildingRepairs;
                MakePaymentToEachPlayer = makePaymentToEachPlayer;
            } // ChanceCard

        } // ChanceCard

        /// <summary>
        /// The collection of all the Chance cards
        /// </summary>
        List<ChanceCard> chanceCards = new List<ChanceCard>();

        /// <summary>
        /// The starting balance for each player.
        /// </summary>
        const int startingAmount = 1500;
        
        /// <summary>
        /// The amount to withdraw for Luxury Tax.
        /// </summary>
        const int luxuryTaxAmount = 75;

        /// <summary>
        /// The amount a Player gets paid in salary for passing GO.
        /// </summary>
        const int salaryAmount = 200;

        /// <summary>
        /// Status StringBuilder.
        /// </summary>
        StringBuilder myStatus = new StringBuilder();

        /// <summary>
        /// The index of the last player.
        /// BANK == 0
        /// </summary>
        int lastPlayerIndex = 0;

        /// <summary>
        /// The current player
        /// </summary>
        Player currentPlayer;

        /// <summary>
        /// Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            
            // Initialize Game
            InitMonopoly();
        } // Form1

        /// <summary>
        /// Initialize the game.
        /// Add Players.
        /// Add Board Spaces.
        /// </summary>
        private void InitMonopoly()
        {
            // Populate Players collection
            //myStatus.AppendLine("Creating players - BANK, Kris, Alexis");
            //myStatus.AppendLine("*********************************************************************************************************************");
            Player _BANK = new Player("BANK", PlayerSymbols.BANK, startingAmount, 0);
            Player _kris = new Player("Kris", PlayerSymbols.Racecar, startingAmount, 0);
            Player _alex = new Player("Alexis", PlayerSymbols.Purse, startingAmount, 0);
            //txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();

            //myStatus.AppendLine("Adding players to players collection - BANK, Kris, Alexis");
            //myStatus.AppendLine("*********************************************************************************************************************");
            players.Add(_BANK);
            players.Add(_kris);
            players.Add(_alex);
           // txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();

            // Populate Board Spaces collection
            //myStatus.AppendLine("PopulateBoardSpaces()");
            //myStatus.AppendLine("*********************************************************************************************************************");
            PopulateBoardSpaces();
            //txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();

            // Set command button status
            cmdDoStuff.Enabled = true;
            cmdBuildHouse.Enabled = false;
            cmdEndTurn.Enabled = false;
            cmdGetStats.Enabled = false;
        } // InitMonopoly

        /// <summary>
        /// Take next turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDoStuff_Click(object sender, EventArgs e)
        {
            // Set command button status
            cmdDoStuff.Enabled = false;
            cmdBuildHouse.Enabled = true;
            cmdEndTurn.Enabled = true;
            cmdGetStats.Enabled = true;

            // Clear status info
            txtStatus.Text = string.Empty;

            // Move to next player index
            lastPlayerIndex++;
            if (lastPlayerIndex > (players.Count - 1)) 
            { 
                lastPlayerIndex = 1;
            } // if

            // Update status bar
            currentPlayer = players.ElementAt(lastPlayerIndex);
            this.Text = string.Format("{0} -- {1}", currentPlayer.PlayerName, currentPlayer.Balance);
            txtStatus.Text += string.Format("Staring turn for {0} with a balance of {1}", currentPlayer.PlayerName, currentPlayer.Balance);

            // Roll dice for player
            Play(currentPlayer);

        } // cmdDoStuff_Click

        private void cmdGetStats_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(GetPlayerStats(currentPlayer), "STATS FOR " + currentPlayer.PlayerName);
            txtStatus.Text += GetPlayerStats(currentPlayer);
        } // cmdGetStats_Click

        private void cmdBuildHouse_Click(object sender, EventArgs e)
        {
            List<BoardSpace> playerBuildableSpaces = GetBuildablePropertiesForPlayer(currentPlayer);

            if (playerBuildableSpaces.Count > 0)
            {
                foreach (BoardSpace buildableSpace in playerBuildableSpaces)
                {
                    if (currentPlayer.Balance >= buildableSpace.BuildingPrice)
                    {
                        string message = string.Format("Would you like to add a house to {0} for a cost of ${1}? You currently have a balance of ${2}.", buildableSpace.SpaceName, buildableSpace.BuildingPrice, currentPlayer.Balance); // "Do you want to close this window?";
                        string title = string.Format("Add a house to {0}?", buildableSpace.SpaceName);

                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result = MessageBox.Show(message, title, buttons);
                        if (result == DialogResult.Yes)
                        {
                            AddHouseToProperty(currentPlayer, buildableSpace);
                        } // if
                        else
                        {
                            // Do nothing...
                        } // else
                    } // if
                    else
                    {
                        string message = string.Format("You cannot afford to build a house on {0} for a cost of ${1}. You currently have a balance of ${2}.", buildableSpace.SpaceName, buildableSpace.BuildingPrice, currentPlayer.Balance); // "Do you want to close this window?";
                        string title = string.Format("Cannot afford to build on {0}", buildableSpace.SpaceName);
                        MessageBox.Show(message, title);
                    } // else
                } // foreach
            } // if
            else
            {
                //MessageBox.Show("You don't own any buildable properties yet.", currentPlayer.PlayerName);
                txtStatus.Text += "You don't own any buildable properties yet.";
            } // else

        } // cmdBuildHouse_Click

        private void cmdEndTurn_Click(object sender, EventArgs e)
        {
            // Clear Status
            myStatus.Clear();
            txtStatus.Text = myStatus.ToString();
            this.Text = string.Empty;

            

            // Set command button status
            cmdEndTurn.Enabled = false;
            cmdDoStuff.Enabled = true;            
            cmdBuildHouse.Enabled = false;
            cmdGetStats.Enabled = false;
        } // cmdEndTurn_Click

        private void cmdBuyProperty_Click(object sender, EventArgs e)
        {
            BoardSpace currentSpace = boardSpaces.ElementAt(currentPlayer.BoardPosition);
            if ((currentSpace.PropertyColor != ColorGroups.Card) && (currentSpace.PropertyColor != ColorGroups.Corner) && (currentSpace.PropertyColor != ColorGroups.Tax))
            {
                // Can the player afford the property?
                if (currentPlayer.Balance >= currentSpace.BuyPrice)
                {
                    // Buy It!
                    myStatus.AppendLine("Player " + currentPlayer.PlayerName + " wants to buy the property " + currentSpace.SpaceName + " for $" + currentSpace.BuyPrice.ToString());
                    txtStatus.Text = myStatus.ToString();
                    //myStatus.Clear();

                    // Buy property from the bank.
                    BuyPropertyFromBank(currentPlayer, currentSpace);
                    cmdBuyProperty.Enabled = false;
                } // if
                else
                {
                    myStatus.AppendLine(string.Format("Player {0} cannot afford to buy property {1} for ${2}", currentPlayer.PlayerName, currentSpace.SpaceName, currentSpace.BuyPrice));
                } // else
            } // if            
        } // cmdBuyProperty_Click

        /// <summary>
        /// Returns a List object containing all the properties the supplied player can build houses on
        /// </summary>
        /// <param name="thePlayer">The Player who wants to build a house.</param>
        /// <returns>A List of BoardSpace of all the Properties the player can build a house on.</returns>
        private List<BoardSpace> GetBuildablePropertiesForPlayer(Player thePlayer)
        { 
            List<BoardSpace> returnList = new List<BoardSpace> ();

            bool canBuildBrown = PlayerOwnsAllPropertiesInGroup(currentPlayer, ColorGroups.Brown);
            bool canBuildLightBlue = PlayerOwnsAllPropertiesInGroup(currentPlayer, ColorGroups.LightBlue);
            bool canBuildPurple = PlayerOwnsAllPropertiesInGroup(currentPlayer, ColorGroups.Purple);
            bool canBuildOrange = PlayerOwnsAllPropertiesInGroup(currentPlayer, ColorGroups.Orange);
            bool canBuildRed = PlayerOwnsAllPropertiesInGroup(currentPlayer, ColorGroups.Red);
            bool canBuildYellow = PlayerOwnsAllPropertiesInGroup(currentPlayer, ColorGroups.Yellow);
            bool canBuildGreen = PlayerOwnsAllPropertiesInGroup(currentPlayer, ColorGroups.Green);
            bool canBuildBlue = PlayerOwnsAllPropertiesInGroup(currentPlayer, ColorGroups.Blue);

            if (canBuildBrown)
            {
                foreach (BoardSpace thisSpace in GetPropertiesInColorGroup(ColorGroups.Brown))
                {
                    if (thisSpace.NumberOfHouses < 5)
                    {
                        returnList.Add(thisSpace);
                    } // if                    
                } // foreach
            } // if

            if (canBuildLightBlue)
            {
                foreach (BoardSpace thisSpace in GetPropertiesInColorGroup(ColorGroups.LightBlue))
                {
                    if (thisSpace.NumberOfHouses < 5)
                    {
                        returnList.Add(thisSpace);
                    } // if
                } // foreach
            } // if

            if (canBuildPurple)
            {
                foreach (BoardSpace thisSpace in GetPropertiesInColorGroup(ColorGroups.Purple))
                {
                    if (thisSpace.NumberOfHouses < 5)
                    {
                        returnList.Add(thisSpace);
                    } // if
                } // foreach
            } // if

            if (canBuildOrange)
            {
                foreach (BoardSpace thisSpace in GetPropertiesInColorGroup(ColorGroups.Orange))
                {
                    if (thisSpace.NumberOfHouses < 5)
                    {
                        returnList.Add(thisSpace);
                    } // if
                } // foreach
            } // if

            if (canBuildRed)
            {
                foreach (BoardSpace thisSpace in GetPropertiesInColorGroup(ColorGroups.Red))
                {
                    if (thisSpace.NumberOfHouses < 5)
                    {
                        returnList.Add(thisSpace);
                    } // if
                } // foreach
            } // if

            if (canBuildYellow)
            {
                foreach (BoardSpace thisSpace in GetPropertiesInColorGroup(ColorGroups.Yellow))
                {
                    if (thisSpace.NumberOfHouses < 5)
                    {
                        returnList.Add(thisSpace);
                    } // if
                } // foreach
            } // if

            if (canBuildGreen)
            {
                foreach (BoardSpace thisSpace in GetPropertiesInColorGroup(ColorGroups.Green))
                {
                    if (thisSpace.NumberOfHouses < 5)
                    {
                        returnList.Add(thisSpace);
                    } // if
                } // foreach
            } // if

            if (canBuildBlue)
            {
                foreach (BoardSpace thisSpace in GetPropertiesInColorGroup(ColorGroups.Blue))
                {
                    if (thisSpace.NumberOfHouses < 5)
                    {
                        returnList.Add(thisSpace);
                    } // if
                } // foreach
            } // if

            return returnList;
        } // GetBuildablePropertiesForPlayer

        /// <summary>
        /// Returns a List object containing all the properties in the supplied Color Group.
        /// </summary>
        /// <param name="theColorGroup">The Color Grouop to return properties for.</param>
        /// <returns>A List of BoardSpace of all the Properties in the specified Color Group.</returns>
        private List<BoardSpace> GetPropertiesInColorGroup(ColorGroups theColorGroup)
        {
            List<BoardSpace> returnList = new List<BoardSpace>();

            foreach (BoardSpace thisSpace in boardSpaces)
            {
                if (thisSpace.PropertyColor == theColorGroup)
                {
                    returnList.Add(thisSpace);
                } // if
            } // foreach
            return returnList;
        } // GetPropertiesInColorGroup

        /// <summary>
        /// Handles a turn for the supplied player.
        /// </summary>
        /// <param name="thePlayer">The Player whose turn it is.</param>
        private void Play(Player thePlayer)
        {
            // Roll dice
            int diceRoll = RollDice();
            myStatus.AppendLine("Player rolled " + diceRoll.ToString());
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();

            // Add dice roll to player's current position to get new position.
            int newPlayerPosition = thePlayer.BoardPosition + diceRoll;
            myStatus.AppendLine("Player's new position: " + newPlayerPosition.ToString());
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();

            // Is the new position beyond #39? (too big?)
            if (newPlayerPosition > 39)
            {
                // Subtract 39 from player's position
                newPlayerPosition -= 39;
                myStatus.AppendLine("Player's new position updated: " + newPlayerPosition.ToString());
                txtStatus.Text = myStatus.ToString();
                //myStatus.Clear();

                // Player passed GO, pay them
                PlayerPassedGo(thePlayer);
                myStatus.AppendLine("PlayerPassedGo");
                txtStatus.Text = myStatus.ToString();
                //myStatus.Clear();
            } // if

            // Update the player's current board position
            thePlayer.BoardPosition = newPlayerPosition;

            PlayerLandedOnProperty(thePlayer);
        } // Play

        /// <summary>
        /// Checks to see whether a player owns all the properties in a given color group.
        /// </summary>
        /// <param name="thePlayer">The Player to evalaute.</param>
        /// <param name="theColorGroup">The Color Group to evaluate.</param>
        /// <returns>TRUE if player owns all properties in the color group, FALSE if not.</returns>
        private bool PlayerOwnsAllPropertiesInGroup(Player thePlayer, ColorGroups theColorGroup)
        {
            myStatus.AppendLine("Checking to see if player " + thePlayer.PlayerName + " owns all properties in group " + theColorGroup.ToString());
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();

            // Create return value
            bool playerOwnsAll = true;
            
            // Loop through all board spaces on the board
            foreach (BoardSpace thisSpace in boardSpaces)
            {
                // Is this space in the correct color group?
                if (thisSpace.PropertyColor == theColorGroup)
                {
                    // Is the owner of this space someone other than the player?
                    if (thisSpace.OwnerName != thePlayer.PlayerName)
                    {
                        // The player does NOT own all the properties in the color group
                        playerOwnsAll = false;
                    } // if
                } // if
            } // foreach

            myStatus.AppendLine("Returning: " + playerOwnsAll.ToString());
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();

            // Return result to caller
            return playerOwnsAll;
        } // PlayerOwnsAllPropertiesInGroup

        /// <summary>
        /// Checks to see how many properties a player owns in a given color group.
        /// </summary>
        /// <param name="thePlayer">The Player to evalaute.</param>
        /// <param name="theColorGroup">The Color Group to evaluate.</param>
        /// <returns>The number of properties owned.</returns>
        private int PlayerOwnsHowManyPropertiesInGroup(Player thePlayer, ColorGroups theColorGroup)
        {
            myStatus.AppendLine("Checking to see how many properties player " + thePlayer.PlayerName + " owns in group " + theColorGroup.ToString());
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();

            // Create return value
            int playerOwnershipTotal = 0;

            // Loop through all board spaces on the board
            foreach (BoardSpace thisSpace in boardSpaces)
            {
                // Is this space in the correct color group?
                if (thisSpace.PropertyColor == theColorGroup)
                {
                    // Is the owner of this space the player?
                    if (thisSpace.OwnerName == thePlayer.PlayerName)
                    {
                        // Increment the number of properties owned by one
                        playerOwnershipTotal++;
                    } // if
                } // if
            } // foreach

            myStatus.AppendLine("Returning: " + playerOwnershipTotal.ToString());
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();

            // Return the final count to the caller
            return playerOwnershipTotal;
        } // PlayerOwnsAllPropertiesInGroup

        /// <summary>
        /// The Player passed the GO space and shall be paid their salary.
        /// </summary>
        /// <param name="thePlayer">The Player to pay.</param>
        private void PlayerPassedGo(Player thePlayer)
        {
            // Update the player's balance
            thePlayer.Balance += salaryAmount;

            myStatus.AppendLine("Paying a salary of " + salaryAmount.ToString() + " to player " + thePlayer.PlayerName + ". They now have a balance of " + thePlayer.Balance.ToString());
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();

        } // PlayerPassedGo

        /// <summary>
        /// Populate the board with board spaces / properties.
        /// </summary>
        private void PopulateBoardSpaces()
        {            
            boardSpaces.Add(new BoardSpace(0, "Go", ColorGroups.Corner, "BANK", -200, 0, 0, 0, 0, 0, 0, 0, 0, false));
            boardSpaces.Add(new BoardSpace(1, "Mediterrian Ave", ColorGroups.Brown, "BANK", 60, 50, 2, 10, 30, 90, 160, 250, 30, false));
            boardSpaces.Add(new BoardSpace(2, "Community Chest", ColorGroups.Card, "BANK", 0, 0, 0, 0, 0, 0, 0, 0, 0, false));
            boardSpaces.Add(new BoardSpace(3, "Baltic Ave", ColorGroups.Brown, "BANK", 60, 50, 4, 20, 60, 180, 320, 450, 30, false));
            boardSpaces.Add(new BoardSpace(4, "Income Tax", ColorGroups.Tax, "BANK", 200, 0, 0, 0, 0, 0, 0, 0, 0, false));
            boardSpaces.Add(new BoardSpace(5, "Reading Railroad", ColorGroups.RailRoad, "BANK", 200, 0, 0, 25, 50, 100, 200, 0, 100, false));
            boardSpaces.Add(new BoardSpace(6, "Oriental Ave", ColorGroups.LightBlue, "BANK", 100, 50, 6, 30, 90, 270, 400, 550, 50, false));
            boardSpaces.Add(new BoardSpace(7, "Chance", ColorGroups.Card, "BANK", 0, 0, 0, 0, 0, 0, 0, 0, 0, false));
            boardSpaces.Add(new BoardSpace(8, "Vermont Ave", ColorGroups.LightBlue, "BANK", 100, 50, 6, 30, 90, 270, 400, 550, 50, false));
            boardSpaces.Add(new BoardSpace(9, "Connecticut Ave", ColorGroups.LightBlue, "BANK", 120, 50, 8, 40, 100, 300, 450, 600, 60, false));
            boardSpaces.Add(new BoardSpace(10, "Jail", ColorGroups.Corner, "BANK", 0, 0, 0, 0, 0, 0, 0, 0, 0, false));
            boardSpaces.Add(new BoardSpace(11, "St. Charles Place", ColorGroups.Purple, "BANK", 140, 100, 10, 50, 150, 450, 625, 750, 70, false));
            boardSpaces.Add(new BoardSpace(12, "Electric Company", ColorGroups.Utility, "BANK", 0, 0, 0, 0, 0, 0, 0, 0, 75, false));
            boardSpaces.Add(new BoardSpace(13, "States Ave", ColorGroups.Purple, "BANK", 140, 100, 10, 50, 150, 450, 625, 750, 70, false));
            boardSpaces.Add(new BoardSpace(14, "Virginia Ave", ColorGroups.Purple, "BANK", 160, 100, 12, 60, 180, 500, 700, 900, 80, false));
            boardSpaces.Add(new BoardSpace(15, "Pennsylvania Railroad", ColorGroups.RailRoad, "BANK", 200, 0, 0, 25, 50, 100, 200, 0, 100, false));
            boardSpaces.Add(new BoardSpace(16, "St. James Place", ColorGroups.Orange, "BANK", 180, 100, 14, 70, 200, 550, 750, 950, 90, false));
            boardSpaces.Add(new BoardSpace(17, "Community Chest", ColorGroups.Card, "BANK", 0, 0, 0, 0, 0, 0, 0, 0, 0, false));
            boardSpaces.Add(new BoardSpace(18, "Tennessee Ave", ColorGroups.Orange, "BANK", 180, 100, 14, 70, 200, 550, 750, 950, 90, false));
            boardSpaces.Add(new BoardSpace(19, "New York Ave", ColorGroups.Orange, "BANK", 200, 100, 16, 80, 220, 600, 800, 1000, 100, false));
            boardSpaces.Add(new BoardSpace(20, "Free Parking", ColorGroups.Corner, "BANK", 0, 0, 0, 0, 0, 0, 0, 0, 0, false));
            boardSpaces.Add(new BoardSpace(21, "Kentucky Ave", ColorGroups.Red, "BANK", 220, 150, 18, 90, 250, 700, 875, 1050, 110, false));
            boardSpaces.Add(new BoardSpace(22, "Chance", ColorGroups.Card, "BANK", 0, 0, 0, 0, 0, 0, 0, 0, 0, false));
            boardSpaces.Add(new BoardSpace(23, "Indiana Ave", ColorGroups.Red, "BANK", 220, 150, 18, 90, 250, 700, 875, 1050, 110, false));
            boardSpaces.Add(new BoardSpace(24, "Illinois Ave", ColorGroups.Red, "BANK", 240, 150, 20, 100, 300, 750, 925, 1100, 120, false));
            boardSpaces.Add(new BoardSpace(25, "B&O Railroad", ColorGroups.RailRoad, "BANK", 200, 0, 0, 25, 50, 100, 200, 0, 100, false));
            boardSpaces.Add(new BoardSpace(26, "Atlantic Ave", ColorGroups.Yellow, "BANK", 260, 150, 22, 110, 330, 800, 975, 1150, 130, false));
            boardSpaces.Add(new BoardSpace(27, "Ventnor Ave", ColorGroups.Yellow, "BANK", 260, 150, 22, 110, 330, 800, 975, 1150, 130, false));
            boardSpaces.Add(new BoardSpace(28, "Water Works", ColorGroups.Utility, "BANK", 0, 0, 0, 0, 0, 0, 0, 0, 75, false));
            boardSpaces.Add(new BoardSpace(29, "Marvin Gardens", ColorGroups.Yellow, "BANK", 260, 200, 24, 120, 360, 850, 1025, 1200, 140, false));
            boardSpaces.Add(new BoardSpace(30, "Go To Jail", ColorGroups.Corner, "BANK", 0, 0, 0, 0, 0, 0, 0, 0, 0, false));
            boardSpaces.Add(new BoardSpace(31, "Pacific Ave", ColorGroups.Green, "BANK", 300, 200, 26, 130, 390, 900, 1100, 1275, 150, false));
            boardSpaces.Add(new BoardSpace(32, "North Carolina Ave", ColorGroups.Green, "BANK", 300, 200, 26, 130, 390, 900, 1100, 1275, 150, false));
            boardSpaces.Add(new BoardSpace(33, "Community Chest", ColorGroups.Card, "BANK", 0, 0, 0, 0, 0, 0, 0, 0, 0, false));
            boardSpaces.Add(new BoardSpace(34, "Pennsylvania Ave", ColorGroups.Green, "BANK", 320, 200, 28, 150, 450, 1000, 1200, 1400, 160, false));
            boardSpaces.Add(new BoardSpace(35, "Short Line Railroad", ColorGroups.RailRoad, "BANK", 200, 0, 0, 25, 50, 100, 200, 0, 100, false));
            boardSpaces.Add(new BoardSpace(36, "Chance", ColorGroups.Card, "BANK", 0, 0, 0, 0, 0, 0, 0, 0, 0, false));
            boardSpaces.Add(new BoardSpace(37, "Park Place", ColorGroups.Blue, "BANK", 350, 200, 35, 175, 500, 1100, 1300, 1500, 175, false));
            boardSpaces.Add(new BoardSpace(38, "Luxury Tax", ColorGroups.Tax, "BANK", 75, 0, 0, 0, 0, 0, 0, 0, 0, false));
            boardSpaces.Add(new BoardSpace(39, "Boardwalk", ColorGroups.Blue, "BANK", 400, 200, 50, 200, 600, 1400, 1700, 2000, 200, false));



            chanceCards.Add(new ChanceCard("Advance to Boardwalk", 39, 0, 0, 0, false, false, false, false, false));
            chanceCards.Add(new ChanceCard("Advance to Go (Collect $200)", 0, 0, 200, 0, false, false, false, false, false));
            chanceCards.Add(new ChanceCard("Advance to Illinois Avenue. If you pass Go, collect $200", 24, 0, 0, 0, false, false, false, false, false));            
            chanceCards.Add(new ChanceCard("Advance to St. Charles Place. If you pass Go, collect $200", 11, 0, 0, 0, false, false, false, false, false));
            chanceCards.Add(new ChanceCard("Advance to the nearest Railroad. If unowned, you may buy it from the Bank. If owned, pay owner twice the rental to which they are otherwise entitled", -1, 0, 0, 0, true, false, false, false, false));
            chanceCards.Add(new ChanceCard("Advance token to nearest Utility. If unowned, you may buy it from the Bank. If owned, throw dice and pay owner a total ten times amount thrown.", -1, 0, 0, 0, false, true, false, false, false));
            chanceCards.Add(new ChanceCard("Bank pays you dividend of $50", -1, 0, 50, 0, false, false, false, false, false));
            chanceCards.Add(new ChanceCard("Get Out of Jail Free", -1, 0, 0, 0, false, false, true, false, false));
            chanceCards.Add(new ChanceCard("Go Back 3 Spaces", -1, -3, 0, 0, false, false, false, false, false));
            chanceCards.Add(new ChanceCard("Go to Jail. Go directly to Jail, do not pass Go, do not collect $200", 10, 0, 0, 0, false, false, false, false, false));
            chanceCards.Add(new ChanceCard("Make general repairs on all your property. For each house pay $25. For each hotel pay $100", -1, 0, 0, 0, false, false, false, true, false));
            chanceCards.Add(new ChanceCard("Speeding fine $15", -1, 0, 0, 15, false, false, false, false, false));
            chanceCards.Add(new ChanceCard("Take a trip to Reading Railroad. If you pass Go, collect $200", 5, 0, 0, 0, false, false, false, false, false));
            chanceCards.Add(new ChanceCard("You have been elected Chairman of the Board. Pay each player $50", -1, 0, 0, 50, false, false, false, false, true));
            chanceCards.Add(new ChanceCard("Your building loan matures. Collect $150", -1, 0, 150, 0, false, false, false, false, false));


        } // PopulateBoardSpaces

        /// <summary>
        /// Simualtes a dice roll.
        /// </summary>
        /// <returns>A value between 1 and 6.</returns>
        private int RollDice()
        {
            // Create the return value
            int returnValue = 0;
            // Set the return value
            returnValue = rnd.Next(1, 7);   // creates a number between 1 and 6

            myStatus.AppendLine("The dice roll was: " + returnValue.ToString());
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();

            // Return the result to the caller
            return returnValue;            
        } // RollDice

        /// <summary>
        /// Gets a Player based on the supplied name.
        /// </summary>
        /// <param name="playerName">The name of the Player to return.</param>
        /// <returns>A Player object representing the requested player.</returns>
        private Player GetPlayerByName(string playerName)
        {
            // Set return object
            Player returnPlayer = players[0];
            // Loop through all game players
            foreach (Player thisPlayer in players)
            {
                // Is this the player name we're looking for?
                if (thisPlayer.PlayerName == playerName)
                {
                    // Set the return object to the player
                    returnPlayer = thisPlayer;
                    // Exit the loop
                    break;
                } // if
            } // foreach

            // Return the requested player
            return returnPlayer;
        } // GetPlayerByName

        /// <summary>
        /// Pays the Luxury Tax.
        /// </summary>
        /// <param name="thePlayer">The Player being taxed.</param>
        private void PayLuxuryTax(Player thePlayer)
        {
            // Debit the Player the luxury tax amount.
            thePlayer.Balance -= luxuryTaxAmount;

            myStatus.AppendLine("Player " + thePlayer.PlayerName + " was just Luxury Taxed " + luxuryTaxAmount.ToString() + ". They now have a balance of " + thePlayer.Balance.ToString());
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();
        } // PayLuxuryTax

        /// <summary>
        /// Pays the Owner of the Utility the amount due.
        /// </summary>
        /// <param name="thePlayer">The Player paying the bill.</param>
        /// <param name="theOwner">The Utility Owner.</param>
        private void PayUtilityBill(Player thePlayer, Player theOwner)
        {
            // How many utilities does this owner own?
            int utilitiesOwnedCount = PlayerOwnsHowManyPropertiesInGroup(theOwner, ColorGroups.Utility);
            // The current bill amount
            int utilityBillAmount = 0;
            // If one utility is owned...
            if (utilitiesOwnedCount == 1)
            {
                // ...the amount due equals dice roll X 4
                utilityBillAmount = RollDice() * 4;
            } // if
            // If two utilities are owned...
            else if (utilitiesOwnedCount == 2)
            {
                // ...the amount due equals dice roll X 10
                utilityBillAmount = RollDice() * 4;
            } // else if
            
            // Debit the player, credit the owner.
            thePlayer.Balance -= utilityBillAmount;
            theOwner.Balance += utilityBillAmount;

            myStatus.AppendLine("Player " + thePlayer.PlayerName + " just paid Utility Bill of " + utilityBillAmount.ToString() + ". They now have a balance of " + thePlayer.Balance.ToString());
            myStatus.AppendLine("Owner " + theOwner.PlayerName + " just got paid a Utility Bill of " + utilityBillAmount.ToString() + ". They now have a balance of " + theOwner.Balance.ToString());
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();
        } // PayUtilityBill

        /// <summary>
        /// Pays the Owner of the Railroad the amount due.
        /// </summary>
        /// <param name="thePlayer">The Player paying the bill.</param>
        /// <param name="theOwner">The Railroad Owner.</param>
        /// <param name="theBoardSpace">The Railroad</param>
        private void PayRailroadBill(Player thePlayer, Player theOwner, BoardSpace theBoardSpace)
        {
            // How many Railroads does this owner own?
            int railroadsOwnedCount = PlayerOwnsHowManyPropertiesInGroup(theOwner, ColorGroups.RailRoad);
            // The current bill amount
            int railroadBillAmount = 0;

            // If the current railroad is not mortgaged, a payment is due
            if (theBoardSpace.IsMortgaged == false)
            {
                // One railroad is owned...
                if (railroadsOwnedCount == 1)
                {
                    // Get the current amount due
                    railroadBillAmount = theBoardSpace.OneHouseRent;
                } // if
                // Two railroads are owned...
                else if (railroadsOwnedCount == 2)
                {
                    // Get the current amount due
                    railroadBillAmount = theBoardSpace.TwoHouseRent;
                } // else if
                // Three railroads are owned...
                else if (railroadsOwnedCount == 3)
                {
                    // Get the current amount due
                    railroadBillAmount = theBoardSpace.ThreeHouseRent;
                } // else if
                // Four railroads are owned...
                else if (railroadsOwnedCount == 4)
                {
                    // Get the current amount due
                    railroadBillAmount = theBoardSpace.FourHouseRent;
                } // else if
            } // if

            // Debit the player, credit the owner.
            thePlayer.Balance -= railroadBillAmount;
            theOwner.Balance += railroadBillAmount;

            myStatus.AppendLine("Player " + thePlayer.PlayerName + " just paid Railroad Bill of " + railroadBillAmount.ToString() + ". They now have a balance of " + thePlayer.Balance.ToString());
            myStatus.AppendLine("Owner " + theOwner.PlayerName + " just got paid a Railroad Bill of " + railroadBillAmount.ToString() + "because they own " + railroadsOwnedCount.ToString() + "railroads. They now have a balance of " + theOwner.Balance.ToString());
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();
        } // PayRailroadBill

        /// <summary>
        /// Pays the Owner of the Property the rent amount due.
        /// </summary>
        /// <param name="thePlayer">The Player paying the rent.</param>
        /// <param name="theOwner">The Property Owner.</param>
        /// <param name="theBoardSpace">The Property.</param>
        private void PayPropertyRent(Player thePlayer, Player theOwner, BoardSpace theBoardSpace)
        {
            // The current bill amount
            int rentAmount = 0;

            // If the current property is not mortgaged, a payment is due
            if (theBoardSpace.IsMortgaged == false)
            {
                // How many houses does the property have?
                switch (theBoardSpace.NumberOfHouses)
                {
                    case 0:
                        rentAmount = theBoardSpace.ZeroHouseRent;
                        break;
                    case 1:
                        rentAmount = theBoardSpace.OneHouseRent;
                        break;
                    case 2:
                        rentAmount = theBoardSpace.TwoHouseRent;
                        break;
                    case 3:
                        rentAmount = theBoardSpace.ThreeHouseRent;
                        break;
                    case 4:
                        rentAmount = theBoardSpace.FourHouseRent;
                        break;
                    case 5:
                        rentAmount = theBoardSpace.HotelRent;
                        break;
                    default:
                        // TODO: throw exception, bad number of houses
                        break;
                } // switch
            } // if

            // Debit the player, credit the owner.
            thePlayer.Balance -= rentAmount;
            theOwner.Balance += rentAmount;

            myStatus.AppendLine(string.Format("Player {0} just paid a rent bill on {1} of ${2} because there are {3} houses. They now have a balance of {4}.", thePlayer.PlayerName, theBoardSpace.SpaceName, rentAmount, theBoardSpace.NumberOfHouses, thePlayer.Balance));
            myStatus.AppendLine(string.Format("Owner {0} just got paid a rent bill on {1} of ${2} because there are {3} houses. They now have a balance of {4}.", theOwner.PlayerName, theBoardSpace.SpaceName, rentAmount, theBoardSpace.NumberOfHouses, theOwner.Balance));
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();
        } // PayPropertyRent

        /// <summary>
        /// Buy a bank-owned property.
        /// </summary>
        /// <param name="thePlayer">The Player purchasing the Property.</param>
        /// <param name="theBoardSpace">The board space being purchased.</param>
        private void BuyPropertyFromBank(Player thePlayer, BoardSpace theBoardSpace)
        {
            // Throw an error if the bank does not currently own the property
            if (theBoardSpace.OwnerName != "BANK")
            {
                // TODO: throw error, bank is not the owner
            } // if
            // Verify the bank currently owns the property
            else
            {
                // Debit the purchase price from the buyer
                thePlayer.Balance -= theBoardSpace.BuyPrice;
                // Update the property owner
                theBoardSpace.OwnerName = thePlayer.PlayerName;

                myStatus.AppendLine("Player " + thePlayer.PlayerName + " is now the owner of " + theBoardSpace.SpaceName);
                txtStatus.Text = myStatus.ToString();
            } // else
            //myStatus.Clear();
        } // BuyPropertyFromBank

        /// <summary>
        /// Build a house on a property.
        /// </summary>
        /// <param name="thePlayer">The Player who owns the property.</param>
        /// <param name="theBoardSpace">The Property being built.</param>
        private void AddHouseToProperty(Player thePlayer, BoardSpace theBoardSpace)
        {
            // Check to make sure the player owns all the properties in the current color group
            if (PlayerOwnsAllPropertiesInGroup(thePlayer, theBoardSpace.PropertyColor))
            {
                // Can the property support more houses?
                if (theBoardSpace.NumberOfHouses < 5)
                {
                    // Debit the player the cost of the house
                    thePlayer.Balance -= theBoardSpace.BuildingPrice;
                    // Add one house to the property
                    theBoardSpace.NumberOfHouses++;
                } // if
                // The property is already full
                else
                {
                    // TODO: throw error, can't build more houses!!!
                } // else
            } // if
            // The player does not own all properties in color group
            else
            {
                // TODO: throw error, player does not own all properties in color group
            } // else

            myStatus.AppendLine("Just added a house to " + theBoardSpace.SpaceName + " for " + thePlayer.PlayerName);
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();
        } // AddHouseToProperty

        /// <summary>
        /// Removes a house from a property.
        /// </summary>
        /// <param name="thePlayer">The Player who owns the property.</param>
        /// <param name="theBoardSpace">The Property being modified.</param>
        private void RemoveHouseFromProperty(Player thePlayer, BoardSpace theBoardSpace)
        {
            // Check to make sure the player owns all the properties in the current color group
            if (PlayerOwnsAllPropertiesInGroup(thePlayer, theBoardSpace.PropertyColor))
            {
                // Are there houses to be removed?
                if (theBoardSpace.NumberOfHouses > 0)
                {
                    // Remove the house from the property
                    theBoardSpace.NumberOfHouses--;
                    // Credit the purchase price back to the property owner
                    thePlayer.Balance += theBoardSpace.BuildingPrice;
                } // if
                // There are no more houses to be removed
                else
                {
                    // TODO: throw error, no houses to remove!!!
                } // else
            } // if
            // The player does not own all properties in color group
            else
            {
                // TODO: throw error, player does not own all properties in color group
            } // else

            myStatus.AppendLine("Just removed a house from " + theBoardSpace.SpaceName + " for " + thePlayer.PlayerName);
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();
        } // RemoveHouseFromProperty



        private void DrawChanceCard(Player thePlayer) 
        {
            // Choose a card
            int myCardChoice = rnd.Next(0, chanceCards.Count);

            ChanceCard myCard = chanceCards.ElementAt(myCardChoice);

            int newBoardSpace = -1;
            BoardSpace theSpace;

            if (myCard.RailRoadDoubleRent)
            {   
                for (int boardSpaceToEvaluate = thePlayer.BoardPosition; boardSpaceToEvaluate < 40; boardSpaceToEvaluate++)
                {
                    // Get the current board space information
                    theSpace = boardSpaces.ElementAt(boardSpaceToEvaluate);

                    // We found a railroad!
                    if (theSpace.PropertyColor == ColorGroups.RailRoad)
                    {
                        newBoardSpace = theSpace.BoardPosition;
                        break;
                    } // if
                } // for

                // We still haven't found a railroad
                if (newBoardSpace == -1)
                {
                    for (int boardSpaceToEvaluate = 0; boardSpaceToEvaluate < 40; boardSpaceToEvaluate++)
                    {
                        // Get the current board space information
                        theSpace = boardSpaces.ElementAt(boardSpaceToEvaluate);

                        // We found a railroad!
                        if (theSpace.PropertyColor == ColorGroups.RailRoad)
                        {
                            newBoardSpace = theSpace.BoardPosition;
                            break;
                        } // if
                    } // for
                } // if

                // Move the player to the railroad
                thePlayer.BoardPosition = newBoardSpace;


                PlayerLandedOnProperty(thePlayer);
                PlayerLandedOnProperty(thePlayer);

            } // if

            if (myCard.Utility10xDiceRoll)
            {

            }


            if (myCard.MakeBuildingRepairs)
            {

            }


            if (myCard.MakePaymentToEachPlayer)
            {

            }


            if (myCard.AdvanceTo == -1)
            {

            }

            if (myCard.CollectPayment > 0)
            {

            }

            if (myCard.IsGetOutOfJailFree)
            {

            }



            if (myCard.MakePayment > 0)
            {

            }


            if (myCard.MoveForwardSpaces > 0)
            {

            }



        } // DrawChanceCard

        /// <summary>
        /// When a player lands on a new board space, this method is called
        /// If the property is for sale, the player can buy it
        /// If the property is owned, rent will be paid
        /// </summary>
        /// <param name="thePlayer">The current Player</param>
        private void PlayerLandedOnProperty(Player thePlayer)
        {
            // Get the current board space the player is on.
            BoardSpace theBoardSpace = boardSpaces[thePlayer.BoardPosition];
            myStatus.AppendLine("The player is on " + theBoardSpace.SpaceName + ". The player is on space #" + theBoardSpace.BoardPosition.ToString());
            myStatus.AppendLine("Evaluating Property Color: " + theBoardSpace.PropertyColor.ToString());
            txtStatus.Text = myStatus.ToString();
            //myStatus.Clear();

            switch (theBoardSpace.PropertyColor)
            {
                // THE CORNERS
                case ColorGroups.Corner:
                    switch (theBoardSpace.SpaceName)
                    {
                        case "Go":
                            myStatus.AppendLine("Player landed on GO");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();
                            break;
                        case "Jail":
                            myStatus.AppendLine("Player landed on Just Visiting Jail");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();
                            break;
                        case "Free Parking":
                            myStatus.AppendLine("Player landed on Free Parking");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();
                            break;
                        case "Go To Jail":
                            myStatus.AppendLine("Player landed on GO TO JAIL");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();
                            // TODO: send player to jail
                            break;
                        default:
                            // TODO: throw Space Name not found error
                            break;
                    } // switch (theBoardSpace.SpaceName)
                    break;

                // THE CARDS
                case ColorGroups.Card:
                    switch (theBoardSpace.SpaceName)
                    {
                        case "Community Chest":
                            myStatus.AppendLine("Player landed on Community Chest");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();
                            // TODO: draw Community Chest card
                            break;
                        case "Chance":
                            myStatus.AppendLine("Player landed on Chance");
                            txtStatus.Text = myStatus.ToString();
                            DrawChanceCard(thePlayer);
                            break;
                        default:
                            // TODO: throw Space Name not found error
                            break;
                    } // switch (theBoardSpace.SpaceName)
                    break;

                // THE TAXES
                case ColorGroups.Tax:
                    switch (theBoardSpace.SpaceName)
                    {
                        case "Luxury Tax":
                            PayLuxuryTax(thePlayer);
                            myStatus.AppendLine("PayLuxuryTax()");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();
                            break;
                        case "Income Tax":
                            myStatus.AppendLine("Player landed on Income Tax");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();
                            // TODO: Income Tax
                            break;
                        default:
                            // TODO: throw Space Name not found error
                            break;
                    } // switch (theBoardSpace.SpaceName)
                    break;

                // THE UTILITIES
                case ColorGroups.Utility:
                    // If the owner is NOT the player...
                    if (theBoardSpace.OwnerName != thePlayer.PlayerName)
                    {
                        if (theBoardSpace.OwnerName == "BANK")
                        {
                            myStatus.AppendLine("Property " + theBoardSpace.SpaceName + " is owned by the BANK and can be purchased.");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();

                            //WantToBuyIt(thePlayer, theBoardSpace);
                            cmdBuyProperty.Enabled = true;
                        } // if
                        else
                        {
                            // Get the current owner
                            Player TheOwner = GetPlayerByName(theBoardSpace.OwnerName);
                            // Pay the current owner
                            PayUtilityBill(thePlayer, TheOwner);

                            myStatus.AppendLine("PayUtilityBill()");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();
                        } // else
                    } // if
                    // The player landed on their own property...
                    else
                    {
                        myStatus.AppendLine("Player landed on their own utility");
                        txtStatus.Text = myStatus.ToString();
                        //myStatus.Clear();
                    } // else
                    break;

                // THE RAILROADS
                case ColorGroups.RailRoad:
                    // If the owner is NOT the player...
                    if (theBoardSpace.OwnerName != thePlayer.PlayerName)
                    {
                        if (theBoardSpace.OwnerName == "BANK")
                        {
                            myStatus.AppendLine("Property " + theBoardSpace.SpaceName + " is owned by the BANK and can be purchased");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();

                            //WantToBuyIt(thePlayer, theBoardSpace);
                            cmdBuyProperty.Enabled = true;
                        } // if
                        else
                        {
                            // Get the current owner
                            Player TheOwner = GetPlayerByName(theBoardSpace.OwnerName);
                            // Pay the current owner
                            PayRailroadBill(thePlayer, TheOwner, theBoardSpace);

                            myStatus.AppendLine("PayRailroadBill()");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();
                        } // else
                    } // if
                    // The player landed on their own property...
                    else
                    {
                        myStatus.AppendLine("Player landed on their own railroad");
                        txtStatus.Text = myStatus.ToString();
                        //myStatus.Clear();
                    } // else
                    break;

                // THE PROPERTIES
                case ColorGroups.Brown:
                case ColorGroups.LightBlue:
                case ColorGroups.Purple:
                case ColorGroups.Orange:
                case ColorGroups.Red:
                case ColorGroups.Yellow:
                case ColorGroups.Green:
                case ColorGroups.Blue:
                    // If the owner is NOT the player...
                    if (theBoardSpace.OwnerName != thePlayer.PlayerName)
                    {
                        if (theBoardSpace.OwnerName == "BANK")
                        {
                            myStatus.AppendLine("Property " + theBoardSpace.SpaceName + " is owned by the BANK and can be purchased");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();

                            //WantToBuyIt(thePlayer, theBoardSpace);
                            cmdBuyProperty.Enabled = true;
                        } // if
                        else
                        {
                            // Get the current owner
                            Player TheOwner = GetPlayerByName(theBoardSpace.OwnerName);
                            // Pay the current owner
                            PayPropertyRent(thePlayer, TheOwner, theBoardSpace);

                            myStatus.AppendLine("PayPropertyRent()");
                            txtStatus.Text = myStatus.ToString();
                            //myStatus.Clear();
                        } // else
                    } // if
                    // The player landed on their own property...
                    else
                    {
                        myStatus.AppendLine("Player landed on their own property");
                        txtStatus.Text = myStatus.ToString();
                        //myStatus.Clear();
                    } // else
                    break;

                // SOMETHING WENT WRONG!!!
                default:
                    // TODO: throw error, invalid color group
                    break;
            } // switch (theBoardSpace.PropertyColor)
        } // PlayerLandedOnProperty




        // TODO: delete?
        /// <summary>
        /// Lets a Player decide whether they want to buy a property or not.
        /// </summary>
        /// <param name="thePlayer">The Player who might make a purchase.</param>
        /// <param name="theBoardSpace">The Property which might be purchased.</param>
        private void WantToBuyIt(Player thePlayer, BoardSpace theBoardSpace)
        {
            // Can the player afford the property?
            if (thePlayer.Balance >= theBoardSpace.BuyPrice)
            {
                string message = string.Format("Do you want to buy {0}({1}) for ${2}?", theBoardSpace.SpaceName, theBoardSpace.PropertyColor, theBoardSpace.BuyPrice);
                string title = string.Format("{0} -- Purchase Property {1}?", thePlayer.PlayerName, theBoardSpace.SpaceName);

                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);

                if (result == DialogResult.Yes)
                {
                    // Buy It!
                    myStatus.AppendLine("Player " + thePlayer.PlayerName + " wants to buy the property " + theBoardSpace.SpaceName + " for $" + theBoardSpace.BuyPrice.ToString());
                    txtStatus.Text = myStatus.ToString();
                    //myStatus.Clear();

                    // Buy property from the bank.
                    BuyPropertyFromBank(thePlayer, theBoardSpace);
                } // if
                else
                {
                    // Close window and do nothing.
                    myStatus.AppendLine("Player " + thePlayer.PlayerName + " DOES NOT WANT to buy the property " + theBoardSpace.SpaceName + " for $" + theBoardSpace.BuyPrice.ToString());
                    txtStatus.Text = myStatus.ToString();
                    //myStatus.Clear();
                } // else
            } // if
            else
            {
                myStatus.AppendLine(string.Format("Player {0} cannot afford to buy property {1} for ${2}", thePlayer.PlayerName, theBoardSpace.SpaceName, theBoardSpace.BuyPrice));
                txtStatus.Text = myStatus.ToString();
                //myStatus.Clear();
            } // else

        } // WantToBuyIt

        private string GetPlayerStats(Player thePlayer) 
        { 
            StringBuilder myStats = new StringBuilder();

            myStats.AppendLine("NAME: " + thePlayer.PlayerName);
            myStats.AppendLine("SYMBOL: " + thePlayer.Symbol.ToString());
            myStats.AppendLine("BALANCE: $" + thePlayer.Balance.ToString());
            myStats.AppendLine("POSITION: " + thePlayer.BoardPosition.ToString());

            int playerPropertiesOwned = 0;
            foreach (BoardSpace thisSpace in boardSpaces)
            {
                if (thisSpace.OwnerName == thePlayer.PlayerName)
                {
                    playerPropertiesOwned++;
                } // if
            } // foreach
            myStats.AppendLine("PROPERTIES: " + playerPropertiesOwned.ToString());

            int utilitiesOwnedCount = PlayerOwnsHowManyPropertiesInGroup(thePlayer, ColorGroups.Utility);
            myStats.AppendLine("UTILITIES: " + utilitiesOwnedCount.ToString());

            int railroadsOwnedCount = PlayerOwnsHowManyPropertiesInGroup(thePlayer, ColorGroups.RailRoad);
            myStats.AppendLine("RAILROADS: " + railroadsOwnedCount.ToString());

            myStats.AppendLine("OWNS ALL BROWN: " + PlayerOwnsAllPropertiesInGroup(thePlayer, ColorGroups.Brown).ToString());
            myStats.AppendLine("OWNS ALL LIGHT BLUE: " + PlayerOwnsAllPropertiesInGroup(thePlayer, ColorGroups.LightBlue).ToString());
            myStats.AppendLine("OWNS ALL PURPLE: " + PlayerOwnsAllPropertiesInGroup(thePlayer, ColorGroups.Purple).ToString());
            myStats.AppendLine("OWNS ALL ORANGE: " + PlayerOwnsAllPropertiesInGroup(thePlayer, ColorGroups.Orange).ToString());
            myStats.AppendLine("OWNS ALL RED: " + PlayerOwnsAllPropertiesInGroup(thePlayer, ColorGroups.Red).ToString());
            myStats.AppendLine("OWNS ALL YELLOW: " + PlayerOwnsAllPropertiesInGroup(thePlayer, ColorGroups.Yellow).ToString());
            myStats.AppendLine("OWNS ALL GREEN: " + PlayerOwnsAllPropertiesInGroup(thePlayer, ColorGroups.Green).ToString());
            myStats.AppendLine("OWNS ALL BLUE: " + PlayerOwnsAllPropertiesInGroup(thePlayer, ColorGroups.Blue).ToString());

            return myStats.ToString();
        } // GetPlayerStats
    } // Form1
} // namespace MonopolySandbox