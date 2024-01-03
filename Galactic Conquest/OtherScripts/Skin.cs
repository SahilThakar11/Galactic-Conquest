using Galactic_Conquest.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.OtherScripts
{
    public class Skin
    {
        
        public int Cost { get; set; }
        public int SkinNumber { get; set; }
        public string Name { get; set; }
        public List<Texture2D> ShipTextures { get; set; }
        public List<Texture2D> ProjectileTextures { get; set; }
        public bool IsOwned { get; set; }

        public Skin(int skinNum,int cost,string name,List<Texture2D> textures, List<Texture2D> projectileTextures)
        {
            SkinNumber = skinNum;
            Cost = cost;
            Name = name;
            ShipTextures = textures;
            ProjectileTextures = projectileTextures;
            
        }
        public void ApplyToPlayer(Player player)
        {
            player.SetTextures(ShipTextures);
            player.SetProjectileTextures(ProjectileTextures);
        }
        
    }
}
