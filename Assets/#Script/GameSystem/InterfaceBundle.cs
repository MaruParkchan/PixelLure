interface IPause
{
    public void Resume();
}

interface ICoroutineStop 
{
    public void CoroutineStop(); // �ڷ�ƾ ����
}

interface ICoroutineStart
{
    public void CorouineStart(); // �ڷ�ƾ ���� or �簳
}
