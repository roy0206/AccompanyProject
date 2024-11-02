using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    float time = 0;
    public void LoadScene(string name)
    {
        StartCoroutine(LoadingAsync(name));
    }

    IEnumerator LoadingAsync(string name)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        asyncOperation.allowSceneActivation = false; //�ε��� �Ϸ�Ǵ´�� ���� Ȱ��ȭ�Ұ�����

        while (!asyncOperation.isDone)
        { //isDone�� �ε��� �Ϸ�Ǿ����� Ȯ���ϴ� ����
            time += Time.deltaTime; //�ð��� ������
            print(asyncOperation.progress); //�ε��� �󸶳� �Ϸ�Ǿ����� 0~1�� ������ ������

            //�̰� �ε��� �ʹ� ���� �ۼ��ѰŶ�, ���ſ� �� �ε��Ҷ� �ð� üũ�ϴ� �κ���
            //�����ص� �����ϴ�!
            if (time > 3)
            { //3�� ��ٸ�(��������)
                asyncOperation.allowSceneActivation = true; //�� Ȱ��ȭ
            }
            yield return null;
        }
    }
}