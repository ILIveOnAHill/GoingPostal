using System;
using System.IO;
using System.Text.Json; 

namespace GoingPostal;

public static class GameDataManager
{  
    public static GameData GameData {get; set;}
    private static string GetSavePath()
    {
        // Shranimo v mapo za lokalne aplikacijske podatke
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string gameFolder = Path.Combine(folder, "GoingPostal");
        
        // Ustvari mapo, če še ne obstaja
        if (!Directory.Exists(gameFolder)) Directory.CreateDirectory(gameFolder);
        
        return Path.Combine(gameFolder, "data.json");
    }

    public static void Save()
    {
        try
        {
            string path = GetSavePath();
            
            // Nastavitve za lepši izpis (Indented)
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(GameData, options);
            
            File.WriteAllText(path, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Napaka pri shranjevanju: {ex.Message}");
        }
    }

    public static void DeleteSaveFile()
    {
        string path = GetSavePath();

        try
        {
            // Preverimo, če datoteka sploh obstaja, preden jo brišemo
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine("Datoteka uspešno izbrisana.");
            }
            else
            {
                Console.WriteLine("Datoteke ni bilo mogoče najti, zato brisanje ni potrebno.");
            }
        }
        catch (Exception ex)
        {
            // Do napake lahko pride, če je datoteka zaklenjena s strani drugega procesa
            Console.WriteLine($"Napaka pri brisanju: {ex.Message}");
        }
    }

    public static GameData Load()
    {
        string path = GetSavePath();
        if (!File.Exists(path)) return new();

        try
        {
            string jsonString = File.ReadAllText(path);
            // Deserializiramo nazaj v slovar
            return JsonSerializer.Deserialize<GameData>(jsonString);
        }
        catch
        {
            return new();
        }
    }
}