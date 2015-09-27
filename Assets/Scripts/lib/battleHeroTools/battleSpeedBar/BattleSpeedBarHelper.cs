﻿using System;
using System.Collections.Generic;
using UnityEngine;
using xy3d.tstd.lib.textureFactory;

namespace xy3d.tstd.lib.battleHeroTools
{
    public class BattleSpeedBarHelper
    {
        private RenderTexture resultTexture;
        public BattleSpeedBarHelper()
        {

        }

        public RenderTexture init()
        {
            resultTexture = new RenderTexture((int)BattleSpeedBar.allTextureWidth, (int)BattleSpeedBar.allTextureHeight, 0);
            return resultTexture;
        }

        public void start(List<string> _vec)
        {
            Graphics.SetRenderTarget(resultTexture);

            GL.LoadPixelMatrix(0, BattleSpeedBar.allTextureWidth, BattleSpeedBar.allTextureHeight, 0);

            for (int i = 0; i < _vec.Count; i++)
            {
                string name = _vec[i];

                int m = (int)(i % BattleSpeedBar.seg);
                int n = (int)(i / BattleSpeedBar.seg);


                Rect rect = new Rect(m * BattleSpeedBar.oneTextureWidth, n * BattleSpeedBar.oneTextureHeight, BattleSpeedBar.oneTextureWidth, BattleSpeedBar.oneTextureHeight);

                Action<Texture> loadCall = delegate(Texture t)
                {
                    Graphics.DrawTexture(rect, t);
                };

                TextureFactory.Instance.GetTexture<Texture>("Assets/PlayGround/icon/" + name + ".png", loadCall, true);

                //Texture texture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/PlayGround/icon/" + name + ".png");

            }
        }

    }
}
