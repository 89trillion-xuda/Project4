# Project4

# FourthProject

1、整体框架

- 层级视图中，每个小块都用空的游戏对象封装，方便禁用和启用
- 脚本代码以MVC层级结构分层



2、资源目录结构

| 目录名称           | 目录内容                       | 父目录     | 其他说明           |
| ------------------ | ------------------------------ | ---------- | ------------------ |
| Resources          | 存放动态加载需要的资源         |            | 处于根目录Assets下 |
| 01.Scenes          | 存放场景                       |            | 处于根目录Assets下 |
| 02.Scripts         | 存放脚本                       |            | 处于根目录Assets下 |
| 03.Sprites         | 存放应用到的精灵体             |            | 处于根目录Assets下 |
| 04.Prefabs         | 存放预设体                     |            | 处于根目录Assets下 |
| 05.Plugs           | 存放插件                       |            | 处于根目录Assets下 |
| ReadMeImg          | 存放技术文档中用到的流程图图片 |            | 处于根目录Assets下 |
| Original resources | 存放提供的所有愿资源的文件夹   |            | 处于根目录Assets下 |
| ReadMeImg          | 存放技术文档中的流程图图片     |            | 处于根目录Assets下 |
| Controller         | 存放控制层脚本代码             | 02.Scripts |                    |
| Model              | 存放实体类                     | 02.Scripts |                    |
| View               | 存放视图层控制代码             | 02.Scripts |                    |



3、界面对象结构拆分

| 结构                     | 结构对象说明                                                 | 父界面对象            | 其他说明 |
| ------------------------ | ------------------------------------------------------------ | --------------------- | -------- |
| SampleScene              | 主场景                                                       |                       |          |
| LeaderBoardRootCanvas    | 排行榜根画布                                                 | SampleScene           |          |
| LeaderBoardRootPanel     | 排行榜根画板                                                 | LeaderBoardRootCanvas |          |
| ScrollRectObject         | 整个排行榜界面对象                                           | LeaderBoardRootPanel  |          |
| CloseObject              | 排行榜下方包含关闭按钮和背景的区域对象                       | LeaderBoardRootPanel  |          |
| BeginObject              | 开始界面对象，包含进入排行榜按钮等                           | LeaderBoardRootPanel  |          |
| PopupsObject             | 弹窗对象                                                     | LeaderBoardRootPanel  |          |
| RankListApiManagerObject | 挂载用于加载Http请求的脚本RankListApiManager.cs，之后将该对象挂载到OSA上 | LeaderBoardRootPanel  |          |
| ContentPanel             | 包含下方可滑动排行榜列表内容的画板                           | ScrollRectObject      |          |
| LeaderBoardHeadObject    | 包含排行榜上方页面元素的画板                                 | ScrollRectObject      |          |
| OSA                      | 实现排行榜内容动态加载的插件对象                             | ContentPanel          |          |



4、代码逻辑分层

| 类                            | 主要职责                                                     | 其他说明         |
| ----------------------------- | ------------------------------------------------------------ | ---------------- |
| 实体层：RankListModel.cs      | 对应每条排名的Json数据实体类                                 | 位于Model下      |
| 实体层：GetRankListModels.cs  | 通过SimpleJson解析Json文件数据，并存储到List链表中，并对得到的list进行倒序排序 | 位于Model下      |
| 控制层：ViewSkipController.cs | 控制视图界面的跳转和弹窗界面的显示隐藏等                     | 位于Controller下 |
| 控制层：RankListRequest.cs    | 用于创建Http请求的链接和发送请求                             | 位于Controller下 |
| 控制层：RankListApiManager.cs | 用于调用Http请求发送的方法，通过回调函数接收返回的Json的string类型数据 | 位于Controller下 |
| 视图层：BasicListAdapter.cs   | 作为OSA插件对象的适配器脚本，实现根据数据进行排行表列表的动态加载和显示 | 位于View下       |



5、存储设计

- Json数据存在于服务端的网址上，客户端通过Http请求链接获得Json数据
- 每条要显示的列表数据通过OSA先进行静态存储，然后显示的时候进行动态读取加载



6、部分功能的实现思想

- 一、如何控制OSA视窗中显示的排名条数为6条：
- 通过控制OSA视窗的大小和参数中的ContentSpacing内容间距来控制视窗中的排名显示数量。
- 二、如何实现倒计时？
- 在Awake（）初始化中开启Time（）协程，编写TIme（）协程方法，通过While循环和WaitForSeconds（）方法实现循环每秒执行一次，设定终止时间为11月30日0点，时间戳为1638201600，通过时间戳相减得到差值，并将差值时间戳转为日期时间类型，通过视图拖动得到视图中对应显示的倒计时时间字段，然后设置为差值时间的相应时间信息。
- 三、如何实现等待从服务端请求到数据后再通过OSA列表调用并显示出？
- 通过Action委托的方式，实现延迟获得数据
- 四、性能优化：
- 不使用public关键字、Find和GetComponent等方法
- 使用OSA进行数据的动态更新和显示



7、关键代码逻辑的流程图

- OSA的适配器类，实现滚动视窗逻辑的脚本代码逻辑：

<img src="https://github.com/89trillion-xuda/Project4/blob/master/Assets/ReadMeImg/Http%E8%AF%B7%E6%B1%82%E7%9A%84%E4%BB%A3%E7%A0%81%E9%80%BB%E8%BE%91%E8%BF%87%E7%A8%8B.png" style="zoom:80%;" />



