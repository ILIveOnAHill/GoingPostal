using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Xna.Framework;

namespace GoingPostal
{
    /// <summary>
    /// Simple parser for Aseprite-style JSON atlases ("frames" -> name -> "frame": {x,y,w,h}).
    /// Returns a dictionary mapping sprite names to source rectangles.
    /// </summary>
    public static class ParseAtlas
    {
        public static Dictionary<string, Rectangle> Parse(string json)
        {
            var regions = new Dictionary<string, Rectangle>(StringComparer.OrdinalIgnoreCase);

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.TryGetProperty("frames", out var frames))
            {
                foreach (var prop in frames.EnumerateObject())
                {
                    var name = prop.Name;
                    if (prop.Value.TryGetProperty("frame", out var frame))
                    {
                        int x = frame.GetProperty("x").GetInt32();
                        int y = frame.GetProperty("y").GetInt32();
                        int w = frame.GetProperty("w").GetInt32();
                        int h = frame.GetProperty("h").GetInt32();

                        regions[name] = new Rectangle(x, y, w, h);
                    }
                }
            }

            return regions;
        }
    }
}