using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class Language : MonoBehaviour {  
    private int Satır;
    public static Language instance = null;
    

    void Start()
    {
        string Lang = Configuration.instance.Lang;
        if (Lang == "EN")
        {
            //Debug.Log("TRANSLATE CALISMADI");// GetComponent<Text>().text = EN.listLine[Satır];
        }
        else
        {
            try { TranslateControl(); }
            catch { }
            
        }
    }
    public void TranslateControl()
    {
        string ENText = GetComponent<Text>().text.ToLower();

        int ListRange = EN.listLine.Count;
       // Debug.Log("Toplam Satır Sayısı: " + ListRange);

        for (int i = 0; i < ListRange; i++)
        {
            if (ENText == EN.listLine[i].ToLower())
            {
                int Satır = i;
               // Debug.Log("Satır numarası: " + i);
                StartTranslate(Satır);
            }

        }
    }

    public void StartTranslate(int Satır)
        {
        string Lang = Configuration.instance.Lang;
        //GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Wrap;
        //GetComponent<Text>().verticalOverflow = VerticalWrapMode.Truncate;
        //GetComponent<Text>().resizeTextForBestFit = true;
        //GetComponent<Text>().alignByGeometry = true;
        //if (Lang == "EN")
        //{
        //  GetComponent<Text>().text = EN.listLine[Satır];
        //}
        //else 
        if (Lang == "FR")
        {
            GetComponent<Text>().text = FR.listLine[Satır];
        }
        else if (Lang == "IT")
        {
            GetComponent<Text>().text = IT.listLine[Satır];
        }
        else if (Lang == "DE")
        {
            GetComponent<Text>().text = DE.listLine[Satır];
        }
        else if (Lang == "KO")
        {
            GetComponent<Text>().text = KO.listLine[Satır];
            //GetComponent<Text>().font = Configuration.instance.KOfont;
        }
        else if (Lang == "ZH")
        {
            GetComponent<Text>().text = ZH.listLine[Satır];
            //GetComponent<Text>().font = Configuration.instance.ZHfont;
        }
        else if (Lang == "JA")
        {
            GetComponent<Text>().text = JA.listLine[Satır];
            //GetComponent<Text>().font = Configuration.instance.JAfont;
        }
        else if (Lang == "PL")
        {
            GetComponent<Text>().text = PL.listLine[Satır];
        }
        else if (Lang == "RU")
        {
            GetComponent<Text>().text = RU.listLine[Satır];
            //GetComponent<Text>().font = Configuration.instance.RUfont;
        }
        else if (Lang == "TR")
        {
            GetComponent<Text>().text = TR.listLine[Satır];
        }
        else if (Lang == "BR")
        {
            GetComponent<Text>().text = BR.listLine[Satır];
        }
        else if (Lang == "ES")
        {
            GetComponent<Text>().text = ES.listLine[Satır];
        }
        else
        {
          //  Debug.Log("TRANSLATE CALISMADI");// GetComponent<Text>().text = EN.listLine[Satır];
        }

    }
    public static void StartGlobalTranslate(UnityEngine.UI.Text Label, int a, string b)
    {
        string ENText = Label.text.ToLower();

        int ListRange = EN.listLine.Count;
      //  Debug.Log("Toplam Satır Sayısı: " + ListRange);

        //Label.horizontalOverflow = HorizontalWrapMode.Wrap;
        //Label.verticalOverflow = VerticalWrapMode.Truncate;
        //Label.resizeTextForBestFit = true;
        //Label.alignByGeometry = true;
        for (int i = 0; i < ListRange; i++)
        {
            string Lang = Configuration.instance.Lang;
            if (ENText == EN.listLine[i].ToLower())
            {

                int Satır = i;
                if (Lang == "EN")
                {
                    if (a == 0) { Label.text = EN.listLine[Satır] + " " + b; }
                    else { Label.text = EN.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "FR")
                {
                    if (a == 0) { Label.text = FR.listLine[Satır] + " " + b; }
                    else { Label.text = FR.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "IT")
                {
                    if (a == 0) { Label.text = IT.listLine[Satır] + " " + b; }
                    else { Label.text = IT.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "DE")
                {
                    if (a == 0) { Label.text = DE.listLine[Satır] + " " + b; }
                    else { Label.text = DE.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "KO")
                {
                    if (a == 0) { Label.text = KO.listLine[Satır] + " " + b; }
                    else { Label.text = KO.listLine[Satır] + " " + a + " " + b; }
                    //Label.font = Configuration.instance.KOfont;
                }
                else if (Lang == "ZH")
                {
                    if (a == 0) { Label.text = ZH.listLine[Satır] + " " + b; }
                    else { Label.text = ZH.listLine[Satır] + " " + a + " " + b; }
                    //Label.font = Configuration.instance.ZHfont;
                }
                else if (Lang == "JA")
                {
                    if (a == 0) { Label.text = JA.listLine[Satır] + " " + b; }
                    else { Label.text = JA.listLine[Satır] + " " + a + " " + b; }
                    //Label.font = Configuration.instance.JAfont;
                }
                else if (Lang == "PL")
                {
                    if (a == 0) { Label.text = PL.listLine[Satır] + " " + b; }
                    else { Label.text = PL.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "RU")
                {
                    if (a == 0) { Label.text = RU.listLine[Satır] + " " + b; }
                    else { Label.text = RU.listLine[Satır] + " " + a + " " + b; }
                    //Label.font = Configuration.instance.RUfont;
                }
                else if (Lang == "TR")
                {
                    if (a == 0) { Label.text = TR.listLine[Satır] + " " + b; }
                    else { Label.text = TR.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "BR")
                {
                    if (a == 0) { Label.text = BR.listLine[Satır] + " " + b; }
                    else { Label.text = BR.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "ES")
                {
                    if (a == 0) { Label.text = ES.listLine[Satır] + " " + b; }
                    else { Label.text = ES.listLine[Satır] + " " + a + " " + b; }
                }
                else
                {
                    if (a == 0) { Label.text = EN.listLine[Satır] + " " + b; }
                    else { Label.text = EN.listLine[Satır] + " " + a + " " + b; }
                }

            }
           
        }
    }
    public static void StartGlobalTranslateWord(Text Label = null, string word = null, int a = 0, string b = null)
    {
        Label.text = ""+ word;
        string ENText = Label.text.ToLower();

        int ListRange = EN.listLine.Count;
        //Debug.Log("Toplam Satır Sayısı: " + ListRange);

        //Label.horizontalOverflow = HorizontalWrapMode.Wrap;
        //Label.verticalOverflow = VerticalWrapMode.Truncate;
        //Label.resizeTextForBestFit = true;
        //Label.alignByGeometry = true;
        for (int i = 0; i < ListRange; i++)
        {
            string Lang = Configuration.instance.Lang;
            if (ENText == EN.listLine[i].ToLower())
            {

                int Satır = i;
                if (Lang == "EN")
                {
                    if (a == 0) { Label.text = EN.listLine[Satır] + " " + b; }
                    else { Label.text = EN.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "FR")
                {
                    if (a == 0) { Label.text = FR.listLine[Satır] + " " + b; }
                    else { Label.text = FR.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "IT")
                {
                    if (a == 0) { Label.text = IT.listLine[Satır] + " " + b; }
                    else { Label.text = IT.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "DE")
                {
                    if (a == 0) { Label.text = DE.listLine[Satır] + " " + b; }
                    else { Label.text = DE.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "KO")
                {
                    if (a == 0) { Label.text = KO.listLine[Satır] + " " + b; }
                    else { Label.text = KO.listLine[Satır] + " " + a + " " + b; }
                    //Label.font = Configuration.instance.KOfont;
                }
                else if (Lang == "ZH")
                {
                    if (a == 0) { Label.text = ZH.listLine[Satır] + " " + b; }
                    else { Label.text = ZH.listLine[Satır] + " " + a + " " + b; }
                    //Label.font = Configuration.instance.ZHfont;
                }
                else if (Lang == "JA")
                {
                    if (a == 0) { Label.text = JA.listLine[Satır] + " " + b; }
                    else { Label.text = JA.listLine[Satır] + " " + a + " " + b; }
                    ////Label.font = Configuration.instance.JAfont;
                }
                else if (Lang == "PL")
                {
                    if (a == 0) { Label.text = PL.listLine[Satır] + " " + b; }
                    else { Label.text = PL.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "RU")
                {
                    if (a == 0) { Label.text = RU.listLine[Satır] + " " + b; }
                    else { Label.text = RU.listLine[Satır] + " " + a + " " + b; }
                    //Label.font = Configuration.instance.RUfont;
                }
                else if (Lang == "TR")
                {
                    if (a == 0) { Label.text = TR.listLine[Satır] + " " + b; }
                    else { Label.text = TR.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "BR")
                {
                    if (a == 0) { Label.text = BR.listLine[Satır] + " " + b; }
                    else { Label.text = BR.listLine[Satır] + " " + a + " " + b; }
                }
                else if (Lang == "ES")
                {
                    if (a == 0) { Label.text = ES.listLine[Satır] + " " + b; }
                    else { Label.text = ES.listLine[Satır] + " " + a + " " + b; }
                }
                else
                {
                    if (a == 0) { Label.text = EN.listLine[Satır] + " " + b; }
                    else { Label.text = EN.listLine[Satır] + " " + a + " " + b; }
                }

            }

        }
    }

    public static string TranslateWord(string word)
    {       
        string NewText = ""; 
        for (int i = 0; i < EN.listLine.Count; i++)
        {
            string Lang = Configuration.instance.Lang;
            if (word.ToLower() == EN.listLine[i].ToLower())
            {

                int Satır = i;
                if (Lang == "EN")
                {
                    NewText = EN.listLine[Satır];
                   
                }
                else if (Lang == "FR")
                {
                    NewText = FR.listLine[Satır];
                }
                else if (Lang == "IT")
                {
                    NewText = IT.listLine[Satır];
                }
                else if (Lang == "DE")
                {
                    NewText = DE.listLine[Satır];
                }
                else if (Lang == "KO")
                {
                    NewText = KO.listLine[Satır];
                }
                else if (Lang == "ZH")
                {
                    NewText = ZH.listLine[Satır];
                }
                else if (Lang == "JA")
                {
                    NewText = JA.listLine[Satır];
                }
                else if (Lang == "PL")
                {
                    NewText = PL.listLine[Satır];
                }
                else if (Lang == "RU")
                {
                    NewText = RU.listLine[Satır];
                }
                else if (Lang == "TR")
                {
                    NewText = TR.listLine[Satır];
                }
                else if (Lang == "BR")
                {
                    NewText = BR.listLine[Satır];
                }
                else if (Lang == "ES")
                {
                    NewText = ES.listLine[Satır];
                }
                else
                {
                    NewText = EN.listLine[Satır];
                }

            }
           
        }
        return NewText;
    }
}
