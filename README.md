# Chat-Chan-Debug
![.NET Core](https://github.com/P2P-Develop/Chat-Chan-Debug/workflows/.NET%20Core/badge.svg)
[![GitHub license](https://img.shields.io/github/license/P2P-Develop/Chat-Chan)](https://github.com/P2P-Develop/Chat-Chan/blob/master/LICENSE)
[![Maintainability](https://api.codeclimate.com/v1/badges/67600ab453eab3b8aab9/maintainability)](https://codeclimate.com/github/P2P-Develop/Chat-Chan-Debug/maintainability)
## Overview
Chat-Chanのデバッグ用コンソールアプリケーション。マルチプラットフォーム対応。  
**Powerlineフォント、またはNerdフォントを必ず使用してください。**  
English -> release docs
## Installation
ファイルを解凍し、その中のexeファイルを開くか、コンソールから起動します。
## Usage
独自コマンド入力形式になっています。
### Commands
- `help` - ヘルプを表示します。
  - エイリアス: `?`
  `help`の後に別のコマンドを入力するとそのコマンドのヘルプが表示されます。
- `background` - 現在のスレッドをバックグラウンドに回します。  
接続中スレッドを操作している場合のみ実行できます。
  - エイリアス: `back`
- `connect [ADDR|HOST] <-u|--user> <-s|--server>'`- 指定されたIPアドレス、またはホストのサーバースレッドに接続します。
  - [ADDR|HOST] - 接続先のIPアドレス、またはホスト名。
  - <-u|--user> - 接続ユーザー名。
  - <-s|--server> - 接続するサーバースレッド。(call, chat, command)
  - エイリアス: `c`
- `disconnect` - 現在の接続スレッドのサーバーから切断します。
  - エイリアス: `dc`
- `getcode` - 現在の接続スレッドの接続状態を取得します。
  - エイリアス: `code`
- `exit` - コンソールを終了します。接続されている場合は切断します。
  - エイリアス: `quit`, `q`
## Contribute Rules
- Pushの際は必ずPull requestにしてください。
