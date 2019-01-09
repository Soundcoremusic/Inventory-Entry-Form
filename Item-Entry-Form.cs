//Assembly: Inventory Entry Form
//Author: Joseph Castrejon
//Description: Windows form application that allows the entry of items for a music store. 
//The entered item has the following fields: Item Type, Manufacturer, Model, Serial Number, Condition, Price and Photos

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace SoundCoreMenus{
	
	//Define enumerations for combobox controls
	public enum ItemCategory {Guitar, Amplifier, Keyboard, Lighting, Microphone, PA, Percussion, Speaker}
	public	enum GuitarManufacturers {Austin, Cordoba, Dean, Epiphone, ESP, Fender, LTD, Martin, Schecter, Sigma, WashBurn, Yamaha}
	public 	enum KeyboardManufacturers {Behringer,Casio,Roland}
	public	enum ItemCondition {BrandNew, Excellent, Good, Poor}
	
	public class App
	{
		//Path which contains all the XML that will be edited by the application
		static string DatabasePath = Application.StartupPath + @"\scdb\";
		private static string PicturesPath = @"E:\Pictures\";
		
	
	
		[STAThread]
		static public void Main()
		{		
			EntryForm StartupForm = new EntryForm();
			StartupForm.ShowDialog();
		}
		
		
		public class EntryForm : Form
		{
			
				DateTime EnteredTime = new DateTime();
				//Common Item types, brands, and manufacturers
				string[] ItemStrings = new string[8]{"Amplifier", "Guitar", "Keyboard", "Lighting", "Microphone", "PA Equipment", "Percussion", "Speaker"};
				string[] Guitar_Brands = new string[13]{"Alvarez", "Austin", "Cordoba", "Dean", "Epiphone", "ESP", "Fender", "LTD","Martin", "Schecter", "Sigma", "Washburn", "Yamaha"};
				string[] Amp_Brands = new string[9]{"Boss", "Lee Jackson", "Egnater", "Fender", "Marshall", "Ampeg", "Crate", "Peavey", "Hartke"};
				string[] Keyboard_Brands = new string[3]{"Casio", "Roland", "Yamaha"};
				string[] Lighting_Brands = new string[1]{"American DJ"};
				string[] Microphone_Brands = new string[4]{"CAD","Shure", "Samson", "Peavey"};
				string[] PA_Brands = new string[9]{"ALESIS","Behringer", "CAD", "MACKIE", "Soundcraft", "Shure", "Samson","Peavey", "Presonus"};
				string[] Percussion_Brands = new string[11]{"DDRUM","CB Percussion","Gretsch","Gibraltar","LP","Mapex","Sabian","Pearl","Pro-Mark","Vic Firth", "Zildjian"};
				string[] Speaker_Brands = new string[2] {"Celestion","JBL"};
				string[] Item_Conditions = new string[4] {"Brand New", "Excellent", "Good", "Poor"};
				private List<string> PhotoFilesList = new List<string>();
				
				//Create the components of the form
				Label FormTitle = new Label();
				Label ItemLabel = new Label();
				Label ManuLabel = new Label();
				Label PriceLabel = new Label();
				Label PhotoLabel = new Label();
				Label SerialNumberLabel = new Label();
				Label DescLabel = new Label();
				Label ConditionLabel = new Label();
				Label ModelLabel = new Label();
				ComboBox ItemType = new ComboBox();
				ComboBox Manufacturer = new ComboBox(); 
				ComboBox ConditionInput = new ComboBox();
				TextBox PriceInput = new TextBox();
				TextBox ModelNumber = new TextBox();
				TextBox SerialNumberInput = new TextBox();
				TextBox Description = new TextBox();
				ListBox PhotoList = new ListBox();
				ButtonBase EnterItem = new Button();
				ButtonBase PhotoSelect = new Button();
				ButtonBase PhotoRemove = new Button();
				PictureBox ItemPreview = new PictureBox();
				
				public EntryForm()
				{
					InitializeComponent();
				}
				
				private void InitializeComponent()
				{
					//Initialize the Components properties
					
					//Default sizes for comboboxes, combobox labels, and buttons 
					Size DefaultButtonSize = new Size(90,50);
					Size DefaultComboBox = new Size(150,20);
					
				
					ItemLabel.Size = DefaultComboBox;
					ItemType.Size = DefaultComboBox;
					ManuLabel.Size = DefaultComboBox;
					Manufacturer.Size = DefaultComboBox;
					PriceLabel.Size = DefaultComboBox;
					PriceInput.Size = DefaultComboBox;
					SerialNumberLabel.Size = DefaultComboBox;
					SerialNumberInput.Size = DefaultComboBox;
					ConditionInput.Size = DefaultComboBox;
					ConditionLabel.Size = DefaultComboBox;
					ModelNumber.Size = DefaultComboBox;
					ModelLabel.Size = DefaultComboBox;
						
					FormTitle.Size = new Size(400,40);
					PhotoLabel.Size = new Size(250,15);
					DescLabel.Size = new Size(260,20);
					Description.Size = new Size(310,80);

					EnterItem.Size = DefaultButtonSize;
					PhotoSelect.Size = DefaultButtonSize;
					PhotoRemove.Size = DefaultButtonSize;
					PhotoList.Size = new Size(250,55);
					ItemPreview.Size = new Size(250,250);
					
					//Text formatting
					PhotoSelect.Text = "Select\nPhotos";
					EnterItem.Text = "\tAdd\n\tItem";
					PhotoRemove.Text = "Remove\nPhotos";
					PhotoLabel.Text = "Current Photo";
					ItemLabel.Text = "Item Type:";
					ManuLabel.Text = "Manufacturer:";
					FormTitle.Text = "Database Item Entry";
					PriceLabel.Text = "Enter Price:";
					SerialNumberLabel.Text = "Serial Number";
					DescLabel.Text = "Enter a short description:";
					ModelLabel.Text = "Model:";
					ConditionLabel.Text = "Item Condtion";
					
					
					Font DefaultInputFont = new Font("Arial", 10);
					Font DefaultLabelFont = new Font("Arial", 12, FontStyle.Bold);
					
					//Font Formatting
					SerialNumberLabel.Font = new Font("Arial", 12,FontStyle.Bold);
					FormTitle.Font = new Font("Arial",24,FontStyle.Bold);
					PhotoLabel.Font = new Font("Arial", 14, FontStyle.Bold);
					EnterItem.Font = new Font("Arial", 11);
					PhotoSelect.Font = new Font("Arial", 10);
					PhotoRemove.Font = new Font("Arial", 10);
					ItemLabel.Font = DefaultLabelFont;
					ManuLabel.Font = DefaultLabelFont;
					PriceLabel.Font = DefaultLabelFont;
					ModelLabel.Font = DefaultLabelFont;
					DescLabel.Font = DefaultLabelFont;
					ConditionLabel.Font = DefaultLabelFont;
					ItemType.Font = DefaultInputFont;
					Manufacturer.Font = DefaultInputFont;
					ConditionInput.Font = DefaultInputFont;
					PriceInput.Font = DefaultInputFont;
					ModelNumber.Font = DefaultInputFont;
					SerialNumberInput.Font = DefaultInputFont;
					PhotoLabel.TextAlign = ContentAlignment.MiddleCenter;
					EnterItem.TextAlign = ContentAlignment.MiddleLeft;
					PhotoSelect.TextAlign = ContentAlignment.MiddleLeft;
					PhotoRemove.TextAlign = ContentAlignment.MiddleLeft;
					
					//Images Controls
					EnterItem.Image = Image.FromFile("Plus-Sign.png");
					EnterItem.ImageAlign = ContentAlignment.MiddleRight;
					PhotoSelect.Image = Image.FromFile("Camera.png");
					PhotoSelect.ImageAlign = ContentAlignment.MiddleRight;
					PhotoRemove.Image = Image.FromFile("Remove-Camera.png");
					PhotoRemove.ImageAlign = ContentAlignment.MiddleRight;
					
					ItemPreview.BorderStyle = BorderStyle.FixedSingle;
					PhotoList.ScrollAlwaysVisible = true;
				
					//Locations
					EnterItem.Location = new Point(10,350);
					PhotoSelect.Location = new Point(120,350);
					PhotoRemove.Location = new Point(230,350);
					FormTitle.Location = new Point (150,10);
					ItemLabel.Location = new Point(10, 70);
					ItemType.Location = new Point(10, 90);
					ManuLabel.Location = new Point(170,70);
					Manufacturer.Location = new Point(170, 90);
					ModelLabel.Location = new Point(10, 130);
					ModelNumber.Location = new Point(10, 150);
					SerialNumberLabel.Location = new Point(170,130);
					SerialNumberInput.Location = new Point(170,150);
					ConditionLabel.Location = new Point(10, 190);
					ConditionInput.Location = new Point(10, 210);
					PriceLabel.Location = new Point(170, 190);
					PriceInput.Location = new Point(170, 210);
			
					PhotoLabel.Location = new Point(330,70);
					PhotoList.Location = new Point(330,350);	
					ItemPreview.Location = new Point(330, 90);
					DescLabel.Location = new Point(10,240);
					Description.Location = new Point(10,260);
					ItemPreview.Image = Image.FromFile("test-image.jpg");

					//Remove user input from combobox
					ItemType.DropDownStyle = ComboBoxStyle.DropDownList;
					Manufacturer.DropDownStyle = ComboBoxStyle.DropDownList;
					ConditionInput.DropDownStyle = ComboBoxStyle.DropDownList;
					
					//Set the max length for description input
					Description.MaxLength = 500;
					
					foreach(string Item in ItemStrings)
						ItemType.Items.Add(Item);
			
					foreach(string Condition in Item_Conditions)
					{
						ConditionInput.Items.Add(Condition);
					}
			
					this.Text = "Sound Core - Database Entry";
					this.StartPosition = FormStartPosition.CenterScreen;
					this.MaximizeBox = false;
					this.MinimizeBox = false;
					this.FormBorderStyle = FormBorderStyle.FixedSingle;
					this.Size = new Size(600,440);
					this.Icon = new Icon("Database.ico", 64,64);	
					this.Controls.Add(SerialNumberLabel);
					this.Controls.Add(FormTitle);
					this.Controls.Add(ItemLabel);
					this.Controls.Add(ItemType);
					this.Controls.Add(Manufacturer);
					this.Controls.Add(ModelNumber);
					this.Controls.Add(SerialNumberInput);
					this.Controls.Add(ConditionInput);
					this.Controls.Add(PriceInput);
					this.Controls.Add(Description);
					this.Controls.Add(ModelLabel);
					this.Controls.Add(ManuLabel);
					this.Controls.Add(ConditionLabel);
					this.Controls.Add(PriceLabel);
					this.Controls.Add(PhotoLabel);
					this.Controls.Add(PhotoSelect);
					this.Controls.Add(PhotoRemove);
					this.Controls.Add(PhotoList);
					this.Controls.Add(EnterItem);
					this.Controls.Add(DescLabel);
					this.Controls.Add(ItemPreview);
					Description.Multiline = true;
					ConditionInput.Enabled = false;
					Manufacturer.Enabled = false;
					ModelNumber.Enabled = false;
					PriceInput.Enabled = false;
					SerialNumberInput.Enabled = false;
					Description.Enabled = false;
				
				
					//DELEGATE METHODS AND EVENT HANDLERS
					ItemType.SelectedValueChanged += new EventHandler(ItemType_SelectedValueChanged);
					Manufacturer.SelectedValueChanged += new EventHandler(Manufacturer_SelectedValueChanged);
					EnterItem.Click += new EventHandler(EnterItem_Click);		
					PhotoSelect.Click += new EventHandler(PhotoSelect_Click);	
					PhotoRemove.Click += new EventHandler(PhotoRemove_Click);
					PhotoList.DoubleClick += new EventHandler(PhotoList_DoubleClick);
					
				}
				
				
				//Event Handlers 
				private void PhotoRemove_Click(object sender, EventArgs e)
				{
					
					try{
						if(PhotoList.GetItemText(PhotoList.SelectedItem) != null)
						{
							int ItemIndex = PhotoList.SelectedIndex;
							this.PhotoList.Items.Remove(PhotoList.SelectedItem);
							//MessageBox.Show(PhotoList.SelectedItem.ToString());
							PhotoFilesList.RemoveAt(ItemIndex);
						}	
					}catch(ArgumentOutOfRangeException RE)
					{
						MessageBox.Show("Please select a photo to remove.", "ERROR: No Photo to remove", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					
				}
				
				private void PhotoSelect_Click(object sender, EventArgs e)
				{
					//Clear the list of files
					
					OpenFileDialog SelectFiles = new OpenFileDialog();
					SelectFiles.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" + "All files (*.*)|*.*";
					SelectFiles.Title = "Select photos of entered item.";
					SelectFiles.Multiselect = true;
					if(SelectFiles.ShowDialog() == DialogResult.OK)
					{
						foreach(string PhotoNames in SelectFiles.FileNames)
						{
							PhotoFilesList.Add(PhotoNames);
							PhotoList.Items.Add(Path.GetFileName(PhotoNames));
						}
						
					}
					
				}
				
				private void ItemType_Click(object sender, EventArgs e)
				{
					
					
				}
				
				private void ItemType_SelectedValueChanged(object sender, EventArgs e)
				{
				
					if(ItemType.SelectedItem != null)
					{
						
						Manufacturer.Enabled = true;
						Manufacturer.Items.Clear();		
						switch(ItemType.SelectedItem.ToString()){
								case "Guitar":
									foreach(string Item in Guitar_Brands)
										Manufacturer.Items.Add(Item);
								break;
								case "Amplifier":
									foreach(string Item in Amp_Brands)
										Manufacturer.Items.Add(Item);
								break;
								case "Keyboard":
									foreach(string Item in Keyboard_Brands)
										Manufacturer.Items.Add(Item);
								break;
								case "Lighting":
									foreach(string Item in Lighting_Brands)
										Manufacturer.Items.Add(Item);
								break;
								case "Microphone":
									foreach(string Item in Microphone_Brands)
										Manufacturer.Items.Add(Item);
								break;
								case "PA Equipment":
									foreach(string Item in PA_Brands)
										Manufacturer.Items.Add(Item);
								break;
								case "Percussion":
									foreach(string Item in Percussion_Brands)
										Manufacturer.Items.Add(Item);
								break;
								case "Speaker":
									foreach(string Item in Speaker_Brands)
										Manufacturer.Items.Add(Item);
								break;
								default:
									MessageBox.Show("ERROR: Item Type not recognized!");
									Application.Exit();
								break;
						};
						
						
					}
					

				}
				
				
				///<summary>
				///
				private void Manufacturer_SelectedValueChanged(object sender, EventArgs e)
				{
					if(Manufacturer.SelectedItem != null)
					{
						//Allow input for the rest of the item controls 	 
						ConditionInput.Enabled = true;
						ModelNumber.Enabled = true;
						PriceInput.Enabled = true;
						SerialNumberInput.Enabled = true;
						Description.Enabled = true;
						
/* 						
						 */
					}
						
					
					
				}
				
				private void EnterItem_Click(object sender, EventArgs e)
				{
					
					bool CorrectInfo = CheckFormFields();
					string ItemInput = ItemType.GetItemText(ItemType.SelectedItem);
					
					
					
					
					
					if(CorrectInfo)
					{
						try{
							
							string FileName;
						
							switch(ItemInput)
							{
								case "Guitar":
									FileName = "Guitars.xml";
									AddItem(FileName);
								break;
								case "Amplifier":
									FileName = "Amplifiers.xml";
									AddItem(FileName);
								break;
								case "Keyboard":
									FileName = "Keyboards.xml";
									AddItem(FileName);
								break;
								case "Lighting":
									FileName = "Lighting.xml";
									AddItem(FileName);
								break;
								case "Microphone":
									FileName = "Microphones.xml";
									AddItem(FileName);
								break;
								case "PA":	
									FileName = "PA-Equipment.xml";
									AddItem(FileName);
								break;
								case "Percussion":
									FileName = "Percussion.xml";
									AddItem(FileName);
								break;
								case "Speaker":
									FileName = "Speakers.xml";
									AddItem(FileName);
								break;
							}
						}catch(Exception Ex)
						{
							
							
						}
						
						ClearFormFields();
					}
				
					
				}
		
		
		
				///<summary>
				///Loads a user specifed XML document, appends an item to the end, and saves the document.
				///</summary>
				private void AddItem(string FileName)
				{
					//Create an empty XML Document
					XDocument XMLDatabaseFile = new XDocument();
					
					//Get the Item type from the form, in order to create the create XML object
					string[] ItemInfo = new string[] {Manufacturer.GetItemText(Manufacturer.SelectedItem),
						ModelNumber.Text,
						SerialNumberInput.Text, 
						Description.Text,
						ConditionInput.GetItemText(ConditionInput.Text)
					};
					
					//Item price check. Checks for negative prices and characters in the price.
					float price = 0;						
					bool CorrectPrice = float.TryParse(PriceInput.Text, out price);
				
					//Create the base Item XML
					XElement ItemXML = new XElement("Item");
					FileStream XmlInfo = new FileStream("./scdb/"+FileName, FileMode.Open);
			
					try
					{
						XMLDatabaseFile = XDocument.Load(XmlInfo);
						XmlInfo.Close();
						//MessageBox.Show("Adding to " + XMLDatabaseFile.Root.Name.ToString() + " Database.");
						ItemXML.Add(new XElement("Manufacturer",ItemInfo[0]));
						ItemXML.Add(new XElement("Model",ItemInfo[1]));
						ItemXML.Add(new XElement("SerialNumber",ItemInfo[2]));
						ItemXML.Add(new XElement("Condition",ItemInfo[4]));
						ItemXML.Add(new XElement("Price", price));
						ItemXML.Add(new XElement("Description",ItemInfo[3]));
						ItemXML.Add(new XElement("DateAdded", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()));
						XElement Pictures = new XElement("ItemPics");
						foreach(var FilePath in PhotoFilesList)
						{
							Pictures.Add(new XElement("Picture", FilePath));
						}
						ItemXML.Add(Pictures);
						XNode LastItem = XMLDatabaseFile.Root.LastNode;
						
						
						if(LastItem == null)
						{
							XMLDatabaseFile.Root.Add(ItemXML);	
							
						}else if(LastItem != null)
						{
							LastItem.AddAfterSelf(ItemXML);
						}
						
						XMLDatabaseFile.Save("./scdb/"+FileName);
					}
					catch(Exception e)
					{
						MessageBox.Show(e.ToString());
						
					}
					
					switch(FileName)
					{
						case "Amplifiers.xml":
							MessageBox.Show("Amplifier Added.");
						break;
						case "Guitars.xml":
							MessageBox.Show("Guitar Added.");
						break;
						case "Keyboards.xml":
							MessageBox.Show("Keyboard Added.");
						break;
						case "Lighting.xml":
							MessageBox.Show("Lighting Added.");
						break;
						case "Microphones.xml":
							MessageBox.Show("Microphones Added.");
						break;
						case "PA-Equipment.xml":
							MessageBox.Show("PA Item Added.");
						break;
						case "Percussion.xml":
							MessageBox.Show("Percussion Item Added.");
						break;
						case "Speakers.xml":
							MessageBox.Show("Speaker Added.");
						break;
						
					}
					
					
					
				}
		
		
				///<summary>
				///Clears the controls of the form so that they are ready for entering a new item.
				///</summary>
				private void ClearFormFields()
				{
					foreach (Control field in this.Controls)
					{
						if (field is TextBox){
							((TextBox)field).Clear();
							((TextBox)field).ForeColor = Color.Black;
							((TextBox)field).Enabled = false;
						}
						else if (field is ComboBox){
							((ComboBox)field).SelectedIndex=-1;
							((ComboBox)field).Enabled = false;
						}
						else if(field is ListBox)
						{
							((ListBox)field).Items.Clear();
						}
						else if(field is PictureBox)
						{
							((PictureBox)field).Image = Image.FromFile("test-image.jpg");
							//Remember to remove all file names from the photo files list or the wrong photos will be associated with the created item.
							this.PhotoFilesList.Clear();
						}
					}
					
					this.ItemType.Enabled = true;
					
				}

				///<summary>
				///Checks the controls of the form for missing information or information that is entered incorrectly.
				///</summary>
				private bool CheckFormFields()
				{
					bool CorrectPrice = false;
					float price = 0;
					//Item price check. Checks for negative prices and characters in the price.	
					CorrectPrice = float.TryParse(PriceInput.Text, out price);
					if (CorrectPrice != true){
						MessageBox.Show("Input price is not a number.","ERROR: Incorrect Price Format",MessageBoxButtons.OK,MessageBoxIcon.Error);
						PriceInput.ForeColor = Color.Red;
						return false;
					}
					
					if (price < 0){
						MessageBox.Show("Price can not be less than zero", "ERROR: Negative Price", MessageBoxButtons.OK, MessageBoxIcon.Error);
						PriceInput.ForeColor = Color.Red;
						return false;
					}
				
					//The price input input is correct, so change the color back to black.
					PriceInput.ForeColor = Color.Black;
				
					//Get the description of the item, make sure it is less than 500 characters.
					//Using less than 500 characters saves on file sizes.
					string DescriptionBuffer;
					DescriptionBuffer = this.Description.Text;
					char[] ItemDescription = new char[500];

					//Check for long descriptions.
					if(DescriptionBuffer.Length > 500)
					{
						Description.ForeColor = Color.Red;
						MessageBox.Show("The current description is too long. Please make sure the description is less than 500 characters", "ERROR: Description Length too long", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
					
					//Check for no photo added.
					if(PhotoFilesList.Count == 0){
						MessageBox.Show("Please add at least one photo for the item that you are currently entering", "ERROR: No Photo", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
					Description.ForeColor = Color.Black;
									
					return true;
				}
	
				///<summary>
				///Displays a photo in the form's picturebox when the user double clicks on a file name within files list.
				///</summary>
				private void PhotoList_DoubleClick(object sender, EventArgs e)
				{
					if(PhotoList.SelectedItem != null)
					{
						var PhotoIndex = PhotoList.SelectedIndex;
						Image ItemImage = Image.FromFile(PhotoFilesList[PhotoIndex]);
						this.ItemPreview.SizeMode = PictureBoxSizeMode.StretchImage;
						this.ItemPreview.Image = ItemImage;
					}
					
					
				}
	
		
		}
		
	}

	//Base Item class
	public class Item
	{
			ItemCategory ID;
			ItemCondition ItemState;
			XmlWriter DocumentWriter;
			
			//Constructor for XML object
			public Item(ItemCategory ItemClass)
			{
				ID = ItemClass;
			}
			
			
			//This method probably needs cleaning up in the future.
			///<summary>
			///Returns the item as a string of XML.
			///</summary>
			public string ToXML(string Manufacturer, string ModelNumber, string SerialNumber,char[] desc,List<string> ItemPictures, string Condition, float price)
			{
				//Default item tag 
				XElement ItemInfo = new XElement("Item");
				
				try{
					
					
					switch(this.ID)
					{
						case ItemCategory.Guitar:
							ItemInfo.Name = "Guitar";
							ItemInfo.Add(new XElement("Manufacturer", Manufacturer));
							ItemInfo.Add(new XElement("Model", ModelNumber));
							ItemInfo.Add(new XElement("SerialNumber", SerialNumber));
							ItemInfo.Add(new XElement("Condition",Condition));
							ItemInfo.Add(new XElement("Price", price));
							ItemInfo.Add(new XElement("ItemDescription", desc)	);
							
							XElement ItemPics = new XElement("ItemPics");
							
							foreach(string FileName in ItemPictures)
							{
								ItemPics.Add(new XElement("Picture", FileName));
							}
							
							ItemInfo.Add(ItemPics);
							
						break;
						case ItemCategory.Amplifier:
							ItemInfo.Name = "Amplifier";
							ItemInfo.Add(new XElement("Manufacturer", Manufacturer));
							ItemInfo.Add(new XElement("Model", ModelNumber));
							ItemInfo.Add(new XElement("SerialNumber", SerialNumber));
							ItemInfo.Add(new XElement("Condition",Condition));
							ItemInfo.Add(new XElement("Price", price));
														
						break;
						case ItemCategory.Speaker:
							ItemInfo.Name = "Speaker";
							ItemInfo.Add(new XElement("Manufacturer", Manufacturer));
							ItemInfo.Add(new XElement("Model", ModelNumber));
							ItemInfo.Add(new XElement("SerialNumber", SerialNumber));
							ItemInfo.Add(new XElement("Condition",Condition));
							ItemInfo.Add(new XElement("Price", price));						
							
						break;
						case ItemCategory.Keyboard:
							ItemInfo.Name = "Keyboard";
							ItemInfo.Add(new XElement("Manufacturer", Manufacturer));
							ItemInfo.Add(new XElement("Model", ModelNumber));
							ItemInfo.Add(new XElement("SerialNumber", SerialNumber));
							ItemInfo.Add(new XElement("Condition",Condition));
							ItemInfo.Add(new XElement("Price", price));						
						
						break;
						case ItemCategory.Microphone:
							ItemInfo.Name = "Microphone";
							ItemInfo.Add(new XElement("Manufacturer", Manufacturer));
							ItemInfo.Add(new XElement("Model", ModelNumber));
							ItemInfo.Add(new XElement("SerialNumber", SerialNumber));
							ItemInfo.Add(new XElement("Condition",Condition));
							ItemInfo.Add(new XElement("Price", price));
							
						break;
						case ItemCategory.PA:
							ItemInfo.Name = "PA";
							ItemInfo.Add(new XElement("Manufacturer", Manufacturer));
							ItemInfo.Add(new XElement("Model", ModelNumber));
							ItemInfo.Add(new XElement("SerialNumber", SerialNumber));
							ItemInfo.Add(new XElement("Condition",Condition));
							ItemInfo.Add(new XElement("Price", price));
							
						break;
						case ItemCategory.Percussion:
							ItemInfo.Name = "Percussion";
							ItemInfo.Add(new XElement("Manufacturer", Manufacturer));
							ItemInfo.Add(new XElement("Model", ModelNumber));
							ItemInfo.Add(new XElement("SerialNumber", SerialNumber));
							ItemInfo.Add(new XElement("Condition",Condition));
							ItemInfo.Add(new XElement("Price", price));
						break;
						case ItemCategory.Lighting:
							ItemInfo.Name = "Lighting";
							ItemInfo.Add(new XElement("Manufacturer", Manufacturer));
							ItemInfo.Add(new XElement("Model", ModelNumber));
							ItemInfo.Add(new XElement("SerialNumber", SerialNumber));
							ItemInfo.Add(new XElement("Condition",Condition));
							ItemInfo.Add(new XElement("Price", price));
							
						break;
						default:
							throw new ArgumentException("Argument Exception: null");
						break;
					}
				}
				catch(Exception e)
				{
					
				}
				finally
				{
	
				}
				
				return ItemInfo.ToString();
			}
			
			
			
	}

}


