using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GithubButton : MonoBehaviour
{
    const string m_RepositoryURI = "https://github.com/Sharlottes/johnLemon";

    public void OnClick()
    {
        Application.OpenURL(m_RepositoryURI);
    }
}
