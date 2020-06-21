# portasLogicas
Programa de simulação de portas lógicas, com fórmulas booleanas e valor de fontes variável. Feito em C#, no Unity. `¯\_(ツ)_/¯`

![portasLogicas](/img/capa.png)

Pra usar a última versão do programa, baixe o arquivo `.zip`, extraia em uma pasta e, rode o `.exe` (não separe o .exe do resto do conteúdo da pasta, a pasta toda é o programa).

Pra poder editar o programa, você precisa criar um projeto novo no Unity e salvar o conteúdo do repositório (menos o `README.md` e o `portasLogicas.zip`)dentro da pasta `Assets`.
Os sprites são brancos pra poder escolher a cor dentro do Unity.

---
Algumas mudanças que eu quero fazer são:
- Perpétuos:
	- diminuir a quantidade de código na função update (melhorar performance).
	- acessibilidade.
	- intuitividade.
	- Diminuir tamanho do programa.
- Já deviam ter sido feitas, mas por algum motivo não foram:
	- botão pra resetar/limpar a área de trabalho.
	- botão pra excluir portas e conectores.
	- adicionar a versão do programa no @/ aumentar o tamanho das letras.
	- não iniciar em tela cheia por padrão.
	- mudar o texto do header do programa.
- Fáceis:
	- alertas de curto-circuito, ligação de duas entradas, etc...
	- modo preto e branco pra impressão, ou pra gente séria.
	- botão na interface pra mudar o esquema de cores.
	- mudar a cor dos conectores, dependendo do valor conectado.
	- *hovertext* na interface.
	- mudar o prefab da porta na área de trabalho.
	- mudar a tela de intro.
	- aba de ferramentas ser retráctil.
	- separar o script de componentes em entradas e saídas?
	- sons?
- Médias:
	- conectores mais intuitivos.
	- tabela de valores
	- botão no menu pra mostrar a tabela de valores.
	- animações no menu e, com um pouco mais de ambição, nas portas.
	- quantidade variável de entradas/saídas nas portas.
	- diminuir o tamanho dos *prefabs* e *sprites*, sem quebrar nada.
- Difíceis:
	- controles com o teclado
	- versão mobile.
	- layout adaptável.
	- script mestre pra centralizar as mudanças de valor.
	- versão pixelada.

---
![academia de letras](/img/letras.jpg)
Todos os arquivos desse projeto (com exceção da fonte tipográfica utilizada) são de minha autoria e portanto podem ser livremente copiados ou editados por qualquer um, pra qualquer fim.