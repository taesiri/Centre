using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ColorBank
    {
        public List<Color> PrimitiveColors;
        public List<Color> SecondaryColors;
        public List<Color> CustomColors;
        public System.Random RndGenerator = new System.Random(DateTime.Now.Millisecond);

        public ColorBank()
        {
            PrimitiveColors = new List<Color>();
            SecondaryColors = new List<Color>();
            CustomColors = new List<Color>();

            GeneratePrimitiveData();
            GenerateSecondaryData();
            GenerateCustomData();
        }

        private void GeneratePrimitiveData()
        {
            PrimitiveColors.Add(Color.white);
            PrimitiveColors.Add(Color.black);
            PrimitiveColors.Add(Color.red);
            PrimitiveColors.Add(Color.green);
            PrimitiveColors.Add(Color.blue);
        }

        private void GenerateSecondaryData()
        {
            SecondaryColors.Add(Color.white);
            SecondaryColors.Add(Color.black);
            SecondaryColors.Add(Color.cyan);
            SecondaryColors.Add(Color.yellow);
            SecondaryColors.Add(Color.magenta);
        }

        private void GenerateCustomData()
        {
            CustomColors.Add(Daffodil);
            CustomColors.Add(Daisy);
            CustomColors.Add(Mustard);
            CustomColors.Add(CitrusZest);
            CustomColors.Add(Pumpkin);
            CustomColors.Add(Tangerine);
            CustomColors.Add(Salmon);
            CustomColors.Add(Persimmon);
            CustomColors.Add(Rouge);
            CustomColors.Add(Scarlet);
            CustomColors.Add(HotPink);
            CustomColors.Add(Princess);
            CustomColors.Add(Petal);
            CustomColors.Add(Lilac);
            CustomColors.Add(Lavender);
            CustomColors.Add(Violet);
            CustomColors.Add(Cloud);
            CustomColors.Add(Dream);
            CustomColors.Add(Gulf);
            CustomColors.Add(Turquoise);
            CustomColors.Add(Sky);
            CustomColors.Add(Indigo);
            CustomColors.Add(Navy);
            CustomColors.Add(SeaFoam);
            CustomColors.Add(Teal);
            CustomColors.Add(Peacock);
            CustomColors.Add(Ceadon);
            CustomColors.Add(Olive);
            CustomColors.Add(Bamboo);
            CustomColors.Add(Grass);
            CustomColors.Add(Kelly);
            CustomColors.Add(Forrest);
            CustomColors.Add(Chocolate);
            CustomColors.Add(TerraCotta);
            CustomColors.Add(Camel);
            CustomColors.Add(Linen);
            CustomColors.Add(Stone);
            CustomColors.Add(Smoke);
            CustomColors.Add(Steel);
            CustomColors.Add(Slate);
            CustomColors.Add(MetallicSilver);
            CustomColors.Add(MetallicGold);
            CustomColors.Add(MetallicCopper);
        }


        public Color GetRandomColor
        {
            get { return CustomColors[RndGenerator.Next(0, CustomColors.Count)]; }
        }

        #region Colors

        public Color Daffodil
        {
            get { return new Color(255, 230, 23); }
        }

        public Color Daisy
        {
            get { return new Color(250, 211, 28); }
        }

        public Color Mustard
        {
            get { return new Color(253, 183, 23); }
        }

        public Color CitrusZest
        {
            get { return new Color(250, 170, 23); }
        }

        public Color Pumpkin
        {
            get { return new Color(241, 117, 63); }
        }

        public Color Tangerine
        {
            get { return new Color(237, 87, 36); }
        }

        public Color Salmon
        {
            get { return new Color(240, 70, 57); }
        }

        public Color Persimmon
        {
            get { return new Color(234, 40, 48); }
        }

        public Color Rouge
        {
            get { return new Color(188, 35, 38); }
        }

        public Color Scarlet
        {
            get { return new Color(140, 12, 3); }
        }

        public Color HotPink
        {
            get { return new Color(229, 24, 93); }
        }

        public Color Princess
        {
            get { return new Color(243, 132, 174); }
        }

        public Color Petal
        {
            get { return new Color(250, 198, 210); }
        }

        public Color Lilac
        {
            get { return new Color(178, 150, 199); }
        }

        public Color Lavender
        {
            get { return new Color(123, 103, 174); }
        }

        public Color Violet
        {
            get { return new Color(95, 53, 119); }
        }

        public Color Cloud
        {
            get { return new Color(195, 222, 241); }
        }

        public Color Dream
        {
            get { return new Color(85, 190, 237); }
        }

        public Color Gulf
        {
            get { return new Color(49, 168, 224); }
        }

        public Color Turquoise
        {
            get { return new Color(35, 138, 204); }
        }

        public Color Sky
        {
            get { return new Color(13, 96, 174); }
        }

        public Color Indigo
        {
            get { return new Color(20, 59, 134); }
        }

        public Color Navy
        {
            get { return new Color(0, 27, 74); }
        }

        public Color SeaFoam
        {
            get { return new Color(125, 205, 194); }
        }

        public Color Teal
        {
            get { return new Color(0, 168, 168); }
        }

        public Color Peacock
        {
            get { return new Color(18, 149, 159); }
        }

        public Color Ceadon
        {
            get { return new Color(193, 209, 138); }
        }

        public Color Olive
        {
            get { return new Color(121, 145, 85); }
        }

        public Color Bamboo
        {
            get { return new Color(128, 188, 66); }
        }

        public Color Grass
        {
            get { return new Color(74, 160, 63); }
        }

        public Color Kelly
        {
            get { return new Color(22, 136, 74); }
        }

        public Color Forrest
        {
            get { return new Color(0, 63, 46); }
        }

        public Color Chocolate
        {
            get { return new Color(56, 30, 17); }
        }

        public Color TerraCotta
        {
            get { return new Color(192, 92, 32); }
        }

        public Color Camel
        {
            get { return new Color(191, 155, 107); }
        }

        public Color Linen
        {
            get { return new Color(223, 212, 167); }
        }

        public Color Stone
        {
            get { return new Color(231, 230, 225); }
        }

        public Color Smoke
        {
            get { return new Color(207, 208, 210); }
        }

        public Color Steel
        {
            get { return new Color(138, 139, 143); }
        }

        public Color Slate
        {
            get { return new Color(119, 133, 144); }
        }

        public Color Charcoal
        {
            get { return new Color(71, 77, 77); }
        }

        public Color Black
        {
            get { return new Color(0, 0, 0); }
        }

        public Color White
        {
            get { return new Color(255, 255, 255); }
        }

        public Color MetallicSilver
        {
            get { return new Color(152, 162, 171); }
        }

        public Color MetallicGold
        {
            get { return new Color(159, 135, 89); }
        }

        public Color MetallicCopper
        {
            get { return new Color(140, 202, 65); }
        }

        #endregion
    }
}