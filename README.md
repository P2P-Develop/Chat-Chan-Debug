# Chat-Chan-Debug
![.NET Core](https://github.com/P2P-Develop/Chat-Chan-Debug/workflows/.NET%20Core/badge.svg)
[![GitHub license](https://img.shields.io/github/license/P2P-Develop/Chat-Chan)](https://github.com/P2P-Develop/Chat-Chan/blob/master/LICENSE)
## Overview
Chat-Chanのデバッグ用コンソールアプリケーション。マルチプラットフォーム対応。  
**Powerlineフォントを必ず使用してください。**  
English -> release docs
## Installation
ファイルを解凍し、その中のexeファイルを開くか、コンソールから起動します。
## Usage
独自コマンド入力形式になっています。
### Commands
- 'help' - ヘルプを表示します。
  - エイリアス: '?'
  'help'の後に別のコマンドを入力するとそのコマンドのヘルプが表示されます。
- 'connect [ADDR|HOST] <-u|--user> <-s|--server>' - 指定されたIPアドレス、またはホストのサーバースレッドに接続します。
  - [ADDR|HOST] - 接続先のIPアドレス、またはホスト名。
  - <-u|--user> - 接続ユーザー名。
  - <-s|--server> - 接続するサーバースレッド。(call, chat, command)
  - エイリアス: 'c'(現在未対応)
- 'exit' - コンソールを終了します。接続されている場合は切断します。
  - エイリアス: 'quit', 'q'
## Contribute Rules
- Pushの際は必ずPull requestにしてください。
