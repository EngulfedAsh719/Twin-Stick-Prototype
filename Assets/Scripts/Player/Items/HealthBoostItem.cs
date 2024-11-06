using UnityEngine;
using Photon.Pun;

public class HealthBoostItem : Item
{
    public int changeAmount;

    public override void Use(GameObject player)
    {
        PhotonView photonView = player.GetComponent<PhotonView>();
        if (photonView != null && photonView.IsMine)
        {
            photonView.RPC("DecreaseHealthRPC", RpcTarget.AllBuffered, changeAmount);
        }
    }
}
