namespace DevTools
{
	public interface ISymmetricKeyEncryption
	{
		byte[] Decrypt(byte[] encryptedbytes, byte[] key);

		byte[] Encrypt(byte[] content, byte[] key);
	}
}