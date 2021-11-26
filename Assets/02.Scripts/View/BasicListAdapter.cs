using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using frame8.Logic.Misc.Other.Extensions;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;


// 您应该将名称空间修改为您自己的名称，或者 - 如果您确定不会发生冲突 - 将其完全删除
namespace BasicListAdapter
{
	// 除了 Start() 之外，您还需要实现 1 个重要的回调：UpdateCellViewsHolder()
	// 看下面的解释
	public class BasicListAdapter : OSA<BaseParamsWithPrefab, MyListItemViewsHolder>
	{
		// 存储数据并在项目计数发生变化时通知适配器的助手
		// 可以迭代，也可以通过 [] 操作符访问其元素
		public SimpleDataHelper<MyListItemModel> Data { get; private set; }
		
		//前三名的背景
		[SerializeField] private Sprite normalRankBg;
		[SerializeField] private Sprite rank3Bg;
		[SerializeField] private Sprite rank2Bg;
		[SerializeField] private Sprite rank1Bg;

		//前三名的奖牌 
		[SerializeField] private Sprite rank1;
		[SerializeField] private Sprite rank2;
		[SerializeField] private Sprite rank3;

		//声明段位图标
		[SerializeField] private Sprite AreanBadge1;
		[SerializeField] private Sprite AreanBadge2;
		[SerializeField] private Sprite AreanBadge3;
		[SerializeField] private Sprite AreanBadge4;
		[SerializeField] private Sprite AreanBadge5;
		[SerializeField] private Sprite AreanBadge6;
		[SerializeField] private Sprite AreanBadge7;
		[SerializeField] private Sprite AreanBadge8;
		
		//获得弹窗
		[SerializeField] private GameObject PopUpsObjClone;
		//弹窗里显示的名字
		[SerializeField] private Text UserNameTxtClone;
		//弹窗里显示的排名
		[SerializeField] private Text RankNumTxtClone;
		
		//获得倒计时时间的相应字段
		[SerializeField] private Text RemainDay;
		[SerializeField] private Text RemainHours;
		[SerializeField] private Text RemainMinute;
		[SerializeField] private Text RemainSecond;
		
		//获得自己的排名
		[SerializeField] private Text MyRankTxt;

		[SerializeField] private Image MyRankImg;
		//获得自己的名称
		[SerializeField] private Text MyNameTxt;
		//获得自己的奖杯数
		[SerializeField] private Text MyTrophyTxt;

		//获得数据请求管理类，用于使用里面的数据
		[SerializeField] private RankListApiManager rankListApiManager;

		#region OSA implementation
		protected override void Awake()
		{
			Data = new SimpleDataHelper<MyListItemModel>(this);

			// 调用它初始化内部数据并准备适配器以处理项目计数变化
			base.Awake();
			
			//启动计算倒计时的协程
			StartCoroutine(Time());
			
			//调用http请求方法
			rankListApiManager.RequestNewRankList(1,18,1,false);

			//通过委托，实现延迟获得数据
			GetRankListModels.EventWriteDataSuccess += SetData;
			
		}

		private void SetData()
		{
			//获得Json数据链表
			List<RankListModel> rankListModels = GetRankListModels.RankListModels;

			// 从您的数据源中检索模型并设置项目数
			RetrieveDataAndUpdate(rankListModels.Count);
		}
		
		IEnumerator Time()
		{
			//循环
			while (true)
			{
				//一秒后再执行一次
				yield return new WaitForSeconds(1);

				//倒计时为0的时候退出循环，不再更新倒计时
				if (GetRankListModels.countDown == 0)
				{
					break;
				}
				
				//标准时间
				DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
				//相差的时间戳转日期时间
				DateTime over = startTime.AddSeconds(GetRankListModels.countDown);
				//将相应的倒计时时间字段显示在视图上
				RemainDay.text = over.Day.ToString();
				RemainHours.text = over.Hour.ToString();
				RemainMinute.text = over.Minute.ToString();
				RemainSecond.text = over.Second.ToString();

				//时间戳--
				GetRankListModels.countDown--;
			}
		}

		// 任何时候之前不可见的项目变得可见时都会调用它，或者在它被创建之后，
		// 或者当任何需要刷新的事情发生时
		// 在这里，您将模型中的数据绑定到项目的视图
		// *对于方法的完整描述，请检查基本实现
		protected override MyListItemViewsHolder CreateViewsHolder(int itemIndex)
		{
			var instance = new MyListItemViewsHolder();

			// 使用这个快捷方式可以避免：
			// - 自己实例化预制件
			// - 启用实例游戏对象
			// - 设置它的索引
			// - 调用它的 CollectViews()
			instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);

			return instance;
		}

		// 任何时候之前不可见的项目变得可见时都会调用它，或者在它被创建之后，
		// 或者当任何需要刷新的事情发生时
		// 在这里，您将模型中的数据绑定到项目的视图
		// *对于方法的完整描述，请检查基本实现
		protected override void UpdateViewsHolder(MyListItemViewsHolder newOrRecycled)
		{
			// 在此回调中，“newOrRecycled.ItemIndex” 保证始终反映
			// 应由此视图持有者表示的项目的索引。您将使用此索引
			// 从你的数据集中检索模型
			MyListItemModel model = Data[newOrRecycled.ItemIndex];
			newOrRecycled.TrophyNumTxt.text = model.TrophyNum.ToString();
			newOrRecycled.NickNameTxt.text = model.NickNameTxt.ToString();
			if (model.RankTxt <= 3)
			{
				//前三名显示特定排名图片，不显示数字排名
				newOrRecycled.RankImg.sprite = model.RankImg;
				newOrRecycled.RankImg.gameObject.SetActive(true);
				//防止资源图片变形
				newOrRecycled.RankImg.SetNativeSize();
			}
			else
			{
				//其他人显示数字排名，没有特定排名图片
				newOrRecycled.RankImg.gameObject.SetActive(false);
				newOrRecycled.RankTxt.gameObject.SetActive(true);
				newOrRecycled.RankTxt.text = model.RankTxt.ToString();
			}

			//每条排行榜添加点击事件
			newOrRecycled.RankListBtn.GetComponent<Button>().onClick.AddListener(() =>
			{
				//显示对话框
				PopUpsObjClone.SetActive(true);
				//显示自己的名字
				UserNameTxtClone.text = model.NickNameTxt;
				//显示自己的奖杯数
				RankNumTxtClone.text = model.RankTxt.ToString();
			});
			//背景图片
			newOrRecycled.RankListBtnBg.sprite = model.RankListBtnBg;
			//段位等级图片
			newOrRecycled.AreanBadgeImg.sprite = model.AreanBadge;
		}
		#endregion

		#region data manipulation
		public void AddItemsAt(int index, IList<MyListItemModel> items)
		{
			Data.InsertItems(index, items);
		}

		public void RemoveItemsFrom(int index, int count)
		{
			Data.RemoveItems(index, count);
		}

		public void SetItems(IList<MyListItemModel> items)
		{
			Data.ResetItems(items);
		}
		#endregion


		// 将数据数量调进携程函数
		void RetrieveDataAndUpdate(int count)
		{
			// 开启携程
			StartCoroutine(FetchMoreItemsFromDataSourceAndUpdate(count));
		}
		
		// 从数据源中检索 <count> 个模型，然后调用 OnDataRetrieved。
		// 在实际情况下，您将查询服务器、数据库或任何数据源，然后调用 OnDataRetrieved
		IEnumerator FetchMoreItemsFromDataSourceAndUpdate(int count)
		{
			// 模拟数据检索延迟
			yield return new WaitForSeconds(.5f);
			var newItems = new MyListItemModel[count];
			
			//获得Json数据
			List<RankListModel> rankListModels = GetRankListModels.RankListModels;

			// 在这里检索您的数据
			for (int i = 0; i < count; ++i)
			{
				var model = new MyListItemModel()
				{
					//名子
					NickNameTxt = rankListModels[i].NickName,
					TrophyNum = rankListModels[i].Trophy
				};
			
				//根据奖杯数量判断显示段位
				if ((rankListModels[i].Trophy) < 1000)
				{
					model.AreanBadge = AreanBadge1;
				}
				else if ((rankListModels[i].Trophy) < 2000)
				{
					model.AreanBadge = AreanBadge2;
				}
				else if ((rankListModels[i].Trophy) < 3000)
				{
					model.AreanBadge = AreanBadge3;
				}
				else if ((rankListModels[i].Trophy) < 4000)
				{
					model.AreanBadge = AreanBadge4;
				}
				else if ((rankListModels[i].Trophy) < 5000)
				{
					model.AreanBadge = AreanBadge5;
				}
				else if ((rankListModels[i].Trophy) < 6000)
				{
					model.AreanBadge = AreanBadge6;
				}
				else if ((rankListModels[i].Trophy) < 7000)
				{
					model.AreanBadge = AreanBadge7;
				}
				else
				{
					model.AreanBadge = AreanBadge8;
				}

				//前三名设置特定的背景和排名图片,数字排名为i
				if (i < 3)
				{
					if (i == 0)
					{
						//设置特定背景
						model.RankListBtnBg = rank1Bg;
						//设置特定排名图片
						model.RankImg = rank1;
						//设置数字排名
						model.RankTxt = i+1;
					}

					if (i == 1)
					{
						model.RankListBtnBg = rank2Bg;
						model.RankImg = rank2;
						model.RankTxt = i+1;
					}

					if (i == 2)
					{
						model.RankListBtnBg = rank3Bg;
						model.RankImg = rank3;
						model.RankTxt = i+1;
					}
				}
				else
				{
					//其他人显示数字排名
					model.RankTxt = i+1;
					//其他人统一背景颜色
					model.RankListBtnBg = normalRankBg;
				}
				
				newItems[i] = model;

				//设置标头Banner中自己的信息
				if (rankListModels[i].uid == 3716954261)
				{
					//根据排名设置自己的排名图片
					if (i < 3)
					{
						MyRankImg.gameObject.SetActive(true);
					}
					if (i == 0)
					{
						MyRankImg.sprite = rank1;
					}else if (i == 1)
					{
						MyRankImg.sprite = rank2;
					}else if (i == 2)
					{
						MyRankImg.sprite = rank3;
					}
					else
					{
						MyRankImg.gameObject.SetActive(false);
						MyRankTxt.gameObject.SetActive(true);
					}
					//设置自己的排名文字
					MyRankTxt.text = (i + 1).ToString();
					//设置自己的名字
					MyNameTxt.text = rankListModels[i].nickName;
					//设置自己的奖杯数
					MyTrophyTxt.text = rankListModels[i].Trophy.ToString();
				}
				
			}

			OnDataRetrieved(newItems);
		}

		void OnDataRetrieved(MyListItemModel[] newItems)
		{
			Data.InsertItemsAtEnd(newItems);
		}
	}

	// Class containing the data associated with an item
	public class MyListItemModel
	{
		//声明匹配的ui组件内容类型
		//Item背景
		public Sprite RankListBtnBg;
		//段位
		public Sprite AreanBadge;
		//奖杯数量
		public int TrophyNum;
		//排名数
		public Sprite RankImg;
		public int RankTxt;
		//排名者名字
		public string NickNameTxt;
		/*
		public string title;
		public Color color;
		*/
	}
	
	public class MyListItemViewsHolder : BaseItemViewsHolder
	{
		//声明用到的UI组件
		public Button RankListBtn;
		public Image RankListBtnBg;
		public Image AreanBadgeImg;
		public Text TrophyNumTxt;
		public Image RankImg;
		public Text RankTxt;
		public Text NickNameTxt;
		
		// 从项目的根游戏对象中检索视图
		public override void CollectViews()
		{
			base.CollectViews();
		
			// GetComponentAtPath 是 frame8.Logic.Misc.Other.Extensions 的一个方便的扩展方法
			// 它从变量的类型推断变量的组件，所以你不需要自己指定它
			root.GetComponentAtPath("RankListBtn",out RankListBtn);
			root.GetComponentAtPath("RankListBtn/RankListBtnBg", out RankListBtnBg);
			root.GetComponentAtPath("RankListBtn/RankListObject/AreanBadgeImg", out AreanBadgeImg);
			root.GetComponentAtPath("RankListBtn/RankListObject/ShadowBgImg/TrophyNumTxt", out TrophyNumTxt);
			root.GetComponentAtPath("RankListBtn/RankListObject/RankImg", out RankImg);
			root.GetComponentAtPath("RankListBtn/RankListObject/RankTxt", out RankTxt);
			root.GetComponentAtPath("RankListBtn/RankListObject/NickNameTxt", out NickNameTxt);
		}
	}
}
