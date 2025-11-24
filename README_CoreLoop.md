<div dir="rtl" lang="he">

<h1>Crisis Point – תהליך ליבה</h1>

<h2>תיאור כללי</h2>
<p>
משחק דו-ממדי בנושא משבר כלכלי. השחקן נע בעולם קניות ובשלב מסוים נכנס
למצב "משבר" שבו עליו לבצע משימות הישרדות קוגניטיביות (כמו שאיבת מים, כביסה, אוכל)
כדי לצבור מטבעות ולצאת מהמשבר. במטלה זו מומש תהליך ליבה של משימת "כביסה":
שאיבת מים מהבאר, הבאתם למכונת הכביסה, איסוף בגדים והעברתם למכונה.
</p>

<h2>תהליך ליבה ממומש</h2>

<h3>תיאור מילולי</h3>
<ol>
  <li>השחקן נע בסצנה באמצעות מקשי התנועה וקפיצה.</li>
  <li>השחקן מפעיל ידית ליד באר. הידית יורדת ועולה, הדלי עולה בהדרגה והחבל מתקצר.</li>
  <li>כאשר הדלי מגיע למעלה, השחקן יכול לאסוף אותו ולהחזיק אותו ביד.</li>
  <li>השחקן ניגש עם הדלי למכונת הכביסה, הדלי נעלם והמכונה "מקבלת מים" ומתחילה לנוע (רטט ימינה/שמאלה).</li>
  <li>רק לאחר שהמכונה קיבלה מים, השחקן יכול לאסוף בגדים מערימת בגדים בסצנה.</li>
  <li>במגע עם ערימת בגדים:
    <ul>
      <li>הערימה שנבחרה נעלמת.</li>
      <li>נוצר אובייקט של בגד קטן (Prefab) שמוחזק ביד השחקן.</li>
    </ul>
  </li>
  <li>השחקן ניגש עם הבגד למכונת הכביסה:
    <ul>
      <li>הבגד שביד נעלם (נחשב כאילו נכנס למכונה).</li>
      <li>ניתן לחזור לערימה אחרת ולקחת בגד נוסף, לפי צורכי המשימה.</li>
    </ul>
  </li>
  <li>במקביל, מערכת המטבעות (PlayerCoins) מעדכנת סכום מטבעות ומציגה אותם ב-UI.
      כמות המטבעות משפיעה על הרקע (צבע, מצב משבר וכו').</li>
</ol>

<h2>מבנה פרויקט (תיקיות)</h2>

<ul>
  <div dir="rtl" lang="he">
  <li><strong>Scripts/Player</strong>
    <ul>
      <div dir="rtl" lang="he">
      <li><code>PlayerMovement2D.cs</code> – תנועה וקפיצה של השחקן.</li>
      <li><code>PlayerTaskInteraction.cs</code> – אינטראקציות עם דלי, מכונה ובגדים.</li>
      <li><code>PlayerCoins.cs</code> – ניהול מטבעות ו-UI.</li>
    </ul>
  </li>
  <li><strong>Scripts/Environment</strong>
    <ul>
      <div dir="rtl" lang="he">
      <li><code>SimpleLever.cs</code> – לוגיקת הידית, הסיבוב, הדלי והחבל.</li>
      <li><code>WashingMachineMover.cs</code> – תנועת מכונת הכביסה כאשר היא "פעילה".</li>
    </ul>
  </li>
  <li><strong>Scripts/Objects</strong>
    <ul>
      <div dir="rtl" lang="he">
      <li><code>ClothesPile.cs</code> ניהול ערימת בגדים.</li>
    </ul>
  </li>
  <li><strong>UI</strong>
    <ul>
      <li>טקסט מטבעות (TextMeshPro) המחובר ל-<code>PlayerCoins</code>.</li>
    </ul>
  </li>
  <li><strong>Prefabs</strong>
    <ul>
      <div dir="rtl" lang="he">
      <li>Player</li>
      <li>Bucket</li>
      <li>WashingMachine</li>
      <li>ClothesPile_* (גדול/בינוני/קטן)</li>
      <li>SmallCloth (הבגד הקטן שמוחזק ביד)</li>
    </ul>
  </li>
</ul>

<h2>תרשים UML (לוגי)</h2>

<p>התרשים הבא מתאר את הקשרים המרכזיים בין המחלקות בתהליך הליבה:</p>

<pre>
+------------------+          +-----------------------+
|   PlayerMovement2D|         |      PlayerCoins      |
|------------------|          |-----------------------|
| - rb: Rigidbody2D|          | - Coins: int          |
| - moveSpeed: float|         |-----------------------|
| - jumpForce: float|         | + AddCoins(int)       |
|------------------|          | + TrySpendCoins(int):bool|
| + FixedUpdate()   |         |                       |
+------------------+          +-----------------------+
          ^
          |
          | uses
          |
+---------------------------+
|   PlayerTaskInteraction   |
|---------------------------|
| - bucketHoldPoint: Transform   |
| - clothesHoldPoint: Transform  |
| - washingMachine: WashingMachineMover |
| - carriedBucket: GameObject    |
| - carriedCloth: GameObject     |
| - hasBucket: bool              |
| - hasCloth: bool               |
|-------------------------------|
| + OnTriggerEnter2D(Collider2D)|
| - TryPickBucket(GameObject)   |
| - TryInteractWithMachine()    |
| - TryPickClothes(GameObject)  |
+-------------------------------+
          |
          | interacts with
          v
+-------------------------+        +-------------------------+
|     SimpleLever         |        |  WashingMachineMover    |
|-------------------------|        |-------------------------|
| - lever: Transform      |        | - isActive: bool        |
| - bucket: Transform     |        | - hasWater: bool        |
| - rope: Transform       |        | - startPosition: Vector3|
|-------------------------|        |-------------------------|
| + OnCollisionEnter2D()  |        | + Activate()            |
| - LeverActionStep()     |        | + Deactivate()          |
| - RaiseBucketStep()     |        | + Update()              |
+-------------------------+        +-------------------------+

          ^
          |
          | condition: hasWater == true
          |
+-------------------------+
|      ClothesPile        |   (אופציונלי, אם משתמשים במנהל אחד לערימה)
|-------------------------|
| - smallClothPrefab: GameObject |
| - usesLeft: int         |
|-------------------------|
| + TakeCloth(): GameObject      |
+-------------------------+
</pre>

<a href="https://itamar-raz-dev-game.itch.io/crisis-point-coregame">itch</a>

