/*
 * Copyright 2016, Gregg Tavares.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 *     * Redistributions of source code must retain the above copyright
 * notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above
 * copyright notice, this list of conditions and the following disclaimer
 * in the documentation and/or other materials provided with the
 * distribution.
 *     * Neither the name of Gregg Tavares. nor the names of its
 * contributors may be used to endorse or promote products derived from
 * this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 * OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ClickAndGetImage : MonoBehaviour {

    public Image[] imagensAReceber;
    //public Vector2 dimensoesImagem = new Vector2(400,326);
    public Sprite defaultSprite;
    public void ChamarImagem()
    {
        PaterlandGlobal.REF_image_instance.ImageAguardandoAtualizacao = this.gameObject;
        GetImage.GetImageFromUserAsync("REF_imagem", "ReceiveImage");
        //GetImage.GetImageFromUserAsync(gameObject.name, "ReceiveImage");
    }

    static string s_dataUrlPrefix_PNG = "data:image/png;base64,";
    static string s_dataUrlPrefix_JPG = "data:image/jpg;base64,";
    public void ReceiveImage(string dataUrl)
    {
        if (dataUrl.StartsWith(s_dataUrlPrefix_PNG)|| dataUrl.StartsWith(s_dataUrlPrefix_JPG))
        {
            byte[] imageData = System.Convert.FromBase64String(dataUrl.Substring(s_dataUrlPrefix_PNG.Length));
            //Debug.Log("Tamanho = " + pngData.Length);

            Texture2D tex = new Texture2D(1, 1); // does the size matter?
            if (tex.LoadImage(imageData))
            {
                if (PaterlandGlobal.REF_image_instance.ImageAguardandoAtualizacao != null)
                {
                    //tex.Resize(Mathf.FloorToInt(dimensoesImagem.x), Mathf.FloorToInt(dimensoesImagem.y));
                    //REF_image_instance.ImageAguardandoAtualizacao.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), REF_image_instance.ImageAguardandoAtualizacao.GetComponent<RectTransform>().pivot);
                    PaterlandGlobal.quebracabeca_da_vez = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), PaterlandGlobal.REF_image_instance.ImageAguardandoAtualizacao.GetComponent<RectTransform>().pivot);
                    PaterlandGlobal.REF_image_instance.ImageAguardandoAtualizacao.GetComponent<Image>().sprite = PaterlandGlobal.quebracabeca_da_vez;
                    //for (int i = 0; i < imagensAReceber.Length; i++)
                    //    imagensAReceber[i].sprite = PaterlandGlobal.quebracabeca_da_vez;
                }
            }
            else
            {
                Debug.LogError("could not decode image");
            }
            PaterlandGlobal.REF_image_instance.ImageAguardandoAtualizacao = null;
        }
        else
        {
            Debug.LogError("Error getting image:" + dataUrl);
            PaterlandGlobal.REF_image_instance.ImageAguardandoAtualizacao = null;
        }
    }
    public void ResetImage()
    {
        PaterlandGlobal.REF_image_instance.ImageAguardandoAtualizacao = this.gameObject;
        PaterlandGlobal.quebracabeca_da_vez = defaultSprite;

        PaterlandGlobal.REF_image_instance.ImageAguardandoAtualizacao.GetComponent<Image>().sprite = PaterlandGlobal.quebracabeca_da_vez;

        //for (int i = 0; i < imagensAReceber.Length; i++)
        //    imagensAReceber[i].sprite = PaterlandGlobal.quebracabeca_da_vez;
    }


}