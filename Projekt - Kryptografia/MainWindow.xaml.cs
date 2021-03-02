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
using System.Security.Cryptography;


namespace Projekt___Kryptografia
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	// Klasa generująca okno
	public partial class MainWindow : Window
	{
		const int ALPHASIZE = 383;
		bool sent = false;

		public MainWindow(){
			popupWindow = new keyPopup();
			popupWindow.ShowDialog();
		
			InitializeComponent();
		}
		private keyPopup popupWindow;
		
		//funkcja generująca i zwracająca tabele 383x383
		private char[][] vignereTab()
		{
			char[][] tab = new char[ALPHASIZE][];
			for (int i = 0; i < ALPHASIZE; i++) {
				tab[i] = new char[ALPHASIZE];
			}
			

			for (char i = '\0'; i < ALPHASIZE; i++)
			{
				for (char j = '\0'; j < ALPHASIZE; j++)
				{
					char val = (char)((i + j) % ALPHASIZE);
					int ival = (int)val;
					tab[i][j] = val;
				}
			}
			return tab;
		}

		//funkcja zamieniająca string w liczbę
		string stringToNumber(string s)
		{
			char[] c = s.ToCharArray();
			int i = 0;
			string n ="";
			while (i < c.Length)
			{
				n += (int)c[i] + "\n";
				i++;
			}

			return n;
		}

		//funkcja dopasowywująca długość klucza do długości wiadomości
		string matchKey(string text, string key) {
			string trueKey = "";

			int i = 0;
			foreach (char c in text)
			{
				if (i == key.Length)
					i = 0;
				trueKey += key.ElementAt(i);
				i++;
			}

			return trueKey;
		}
		//funckaj generująca szyfrogram
		string generateCipher(string text, string key) 
		{
			char[] trueKey = matchKey(text, key).ToCharArray();
			char[] trueText = text.ToCharArray();

			char[][] tab = vignereTab();
			
			string cipher = "";

			for (int i=0; i<text.Length; i++)
			{
				int t= trueText[i];
				int k = trueKey[i];
				cipher += tab[t][k];
			}
			return cipher;
		}

		//funckja deszyfrująca
		string decipherMessage(string cipher, string key) {
			char[][] tab = vignereTab();
			char[] trueKey = matchKey(cipher, key).ToCharArray();
			char[] trueCipher = cipher.ToCharArray();

			string message = "";
	
			for (int i = 0; i < trueKey.Length; i++)
			{
				char[] row = tab[trueKey[i]];
				char symbol = (char) Array.FindIndex(row, c => c == trueCipher[i]);
				message += symbol;
			}
			return message;
		}

		//dalej już tylko funkcje pozwalające obsługiwać program w oknie
		private int encrypt(string text)
		{
			secondMessageBox.Text = "";
			secondEncryptedMessage.Text = "";
			firstEncryptedMessage.Text = "";
			sent = false;

			firstEncryptedMessage.Text = generateCipher(firstMessageBox.Text, popupWindow.iKey);
			firstMessageBox.Text = "";
			return 0;
		}

		private int send()
		{
			sent = true;
			return 0;
		}
		private int receive()
		{
			if (sent == true)
			{
				secondEncryptedMessage.Text = firstEncryptedMessage.Text;
				firstEncryptedMessage.Text = "";
			}
			return 0;
		}
		private int decrypt()
		{
			if (secondEncryptedMessage.Text == "")
				return 1;
			else
			{
				secondMessageBox.Text = decipherMessage(secondEncryptedMessage.Text, popupWindow.iKey); ;
				secondEncryptedMessage.Text = "";
				return 0;

			}
			
		}

		private void encryptButton_Click(object sender, RoutedEventArgs e)
		{
			encrypt(firstMessageBox.Text);
		}

		private void sendButton_Click(object sender, RoutedEventArgs e)
		{
			send();
		}

		private void receiveButton_Click(object sender, RoutedEventArgs e)
		{
			receive();
		}

		private void decryptButton_Click(object sender, RoutedEventArgs e)
		{
			decrypt();
		}
		
	}
}
