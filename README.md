# AviUtlExoEditor.LabelCut

AviUtl の拡張編集エクスポートファイルを、Audacity のラベルファイルに基づいて編集するツールです。

## 解説動画

<iframe width="560" height="315" src="https://www.youtube-nocookie.com/embed/IaQ_K2oG67U" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

## 使い方

1. 動画ファイルから、映像と音声を別ファイルにする

2. 音声ファイルを Audacity に読み込ませる

3. 取り込んだ音声を全選択 (Ctrl+A) して、解析タブ → 音声から自動ラベル付けを選択し、適応。

4. ファイルタブ → 書き出し → ラベルの書き出し、でラベルを保存する。(1)

5. AviUtl を起動する。

6. 動画ファイルを AviUtl の拡張編集タイムラインに読み込む。

7. 拡張編集タイムラインを右クリック、ファイル → オブジェクトファイルのエクスポートを選択し、保存する。(2)

8. LabelCut（本ソフト）を起動し、Exo ファイルパスに（2）を、ラベルファイルパスに（1）のパスを指定し、それぞれ読み込む。

9. 変換（中央のボタン）を押し、保存（最下部のボタン）を押して保存。(3)

10. AviUtl を再び開き、（3）を拡張編集タイムラインに読み込む（ドロップアンドドロップ）

11. 完成

## 開発メモ

以下、開発は VS Code にクローンして作業を行う。

### 起動方法(デバッグ)

ターミナルで App.xaml.cs が存在するディレクトリに移動して、

```shell
dotnet run
```

### ビルド

`/bin/Debug/net6.0-windows`に実行可能ファイルが生成される。

```shell
dotnet build
```
