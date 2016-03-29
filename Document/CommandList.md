
#### **`git`** コマンドの一覧

- [`clone`](#L01)
- [`status`](#L02)
- [`add`](#L03)
- [`reset`](#L04)
- [`commit`](#L05)
- [`push`](#L06)
- [`pull`](#L07)
- [`remote`](#L08)

---
**`clone`** <a name = "L01">  
**`github` のリポジトリを自分の環境に新しくコピーする**

~~~
git clone https://github.com/XXX/YYY.git

# XXX: アカウント名
# YYY: リポジトリ名
~~~

---
**`status`** <a name = "L02">  
**環境の変更内容を表示する**

~~~
git status
~~~

変更内容は **赤** 、`add` したファイルは **緑** で表示される

---
**`add`** <a name = "L03">  
**コミット（`commit`）したい変更内容を登録する**

~~~
git add hoge.cs
git add Assets/Scripts/
git add .

# (1) "hoge.cs" のみを登録する
# (2) "Scripts" フォルダの内容を全て登録する
# (3) 全ての変更内容を登録する
~~~

---
**`reset`** <a name = "L04">  
**間違えて `add` した内容を取り消す**

~~~
git reset XXX

# XXX: 間違えて登録したファイル、またはフォルダ
~~~

`add` していないファイルなどを指定すると、**変更内容** が取り消されるので要注意

---
**`commit -m`** <a name = "L05">  
**登録された（`add`）変更内容を `github` に送信（`push`）できる状態にする**

~~~
git commit -m 'コメント'    // シングルクォート
git commit -m "コメント"    // ダブルクォート
~~~

---
**`push`** <a name = "L06">  
**送信できる状態にした（`commit`）内容を `github` に送信する**

~~~
git push XXX YYY

# XXX: リモート名。基本的に origin
# YYY: ブランチ名。基本的に master

ex) git push origin master
~~~

コマンド入力後、自分の `github` アカウント名とパスワードの入力を求められる

---
**`pull`** <a name = "L07">  
**`github` リポジトリの内容を自分の環境に反映する**

~~~
git pull XXX YYY

# XXX: リモート名。基本的に origin または upstream
# YYY: ブランチ名。基本的に master

ex) git pull upstream master
~~~

---
**`remote`** <a name = "L08">  
**現在管理している環境と `github` のリポジトリとのリンクを操作する**

~~~
1. リンクしているリモート名とリポジトリのアドレスを一覧にして表示
git remote -v

2. リモート名の追加とリポジトリのリンク
git remote add XXX https://github.com/YYY/ZZZ.git

# XXX: 新しく追加するリモート名
# YYY: アカウント名
# ZZZ: リポジトリ名
~~~

---
