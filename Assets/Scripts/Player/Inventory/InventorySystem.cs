using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Photon.Pun;

public class InventorySystem : MonoBehaviour
{
    [Header("Inventory Settings")]

    public static InventorySystem Instance { get; set; }
    public GameObject[] buttons;

    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject slotPrefab;

    [SerializeField] private  Transform slotParent;

    private List<Item> items = new List<Item>();
    private List<Item> selectedItems = new List<Item>();

    private int columns = 10;
    private float slotSize = 100f;
    private float padding = 50f;
    private bool isInventoryActive = false;
    private Vector2 startPosition = new Vector2(55, -50);

    private PhotonView photonView;
    public Animator animator;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        inventoryPanel.SetActive(false);
        
        photonView = GetComponentInParent<PhotonView>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            playerCanvas.SetActive(false);
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        UpdateInventoryUI();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        UpdateInventoryUI();
    }

    public void ChangeInventoryState()
    {
        switch (isInventoryActive)
        {
            case false:
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].SetActive(false);
                }

                inventoryPanel.SetActive(true);
                animator.SetTrigger("Open");
                break;
            case true:
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].SetActive(true);
                }
                
                animator.SetTrigger("Close");
                StartCoroutine(CloseCoroutine());
                break;
        }

        isInventoryActive = !isInventoryActive;
    }

    private void UpdateInventoryUI()
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < items.Count; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.GetComponent<Image>().sprite = items[i].icon;
            RectTransform slotRectTransform = slot.GetComponent<RectTransform>();
            int row = i / columns;
            int column = i % columns;
            slotRectTransform.anchoredPosition = new Vector2(startPosition.x + column * (slotSize + padding), startPosition.y - row * (slotSize + padding));

            Button slotButton = slot.GetComponent<Button>();
            if (slotButton == null)
            {
                slotButton = slot.AddComponent<Button>();
            }

            int index = i; 
            slotButton.onClick.AddListener(() => OnSlotClicked(index));
        }
    }

    private void OnSlotClicked(int index)
    {
        Item item = items[index];
        if (selectedItems.Contains(item))
        {
            selectedItems.Remove(item);
        }
        else
        {
            selectedItems.Add(item);
        }
        
        Debug.Log($"Число выбранных предметов: {selectedItems.Count}");
    }

    private IEnumerator CloseCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
    
        inventoryPanel.SetActive(false);
    }
}
