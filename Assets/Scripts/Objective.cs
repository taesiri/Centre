using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Objective : MonoBehaviour
    {
        public int NumberOfColors = 5;
        private readonly ColorBank _myBank = new ColorBank();
        public System.Random RndGenerator = new System.Random(DateTime.Now.Millisecond);

        public void Start()
        {
            for (int index = 0; index < _myBank.CustomColors.Count; index++)
            {
                var customColor = _myBank.CustomColors[index];
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = new Vector3(.65f, .65f, .65f);
                cube.transform.position = new Vector3(-6 + ((index%10)*.85f), .85f*(index/10), 0);
                cube.renderer.material.color = new Color(customColor.r/255, customColor.g/255, customColor.b/255);
            }

            GenerateNewColor();
        }

        private void GenerateNewColor()
        {
            var top = 1000;
            var colors = new List<Color>();
            var coeffiecents = new List<float>();

            var lastRndGenerated = RndGenerator.Next(1, top);

            for (int i = 1; i < NumberOfColors; i++)
            {
                coeffiecents.Add(lastRndGenerated/1000f);
                colors.Add(_myBank.GetRandomColor);

                top -= lastRndGenerated;
                lastRndGenerated = RndGenerator.Next(1, top);
            }

            colors.Add(_myBank.GetRandomColor);
            coeffiecents.Add(top/1000f);

            var r = 0f;
            var g = 0f;
            var b = 0f;

            for (int i = 0; i < colors.Count; i++)
            {
                r += coeffiecents[i]*colors[i].r;
                g += coeffiecents[i]*colors[i].g;
                b += coeffiecents[i]*colors[i].b;
            }

            r /= NumberOfColors;
            g /= NumberOfColors;
            b /= NumberOfColors;

            renderer.material.color = new Color(r/255f, g/255f, b/255f);
        }
    }
}