﻿using UnityEngine;
    {
        controllerToCharacter.Add(new Vector2(controllerNum, characterNum));
        isSelectedCharacter[characterNum] = true;
        totalSelectedChars++;
    }
    {
        possibleCharacters[characterNum].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
    }