using System;
using System.Collections.Generic;
using PunchCars.ShopMVP;
using Zenject;

public interface IShopView
{
    [Inject]
    public void Construct(ShopPresenter presenter);

    public List<CoinsShopItem> CreateCoinsItems(int count);

    void Show();
    void Hide();

    event Action OnShow;
    event Action OnHide;
}
