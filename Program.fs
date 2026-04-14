// For more information see https://aka.ms/fsharp-console-apps
type Expr =
  | Number of int
  | Symbol of string
  | List of Expr list

type Value =
  | Int of int
  | Primitive of (Value list -> Value)
  | Closure of string list * Expr list * Env
and Env = Map<string, Value>

let asInt v =
  match v with
  | Int n -> n
  | _ -> failwith "expected int"

let primitiveAdd args =
  args
  |> List.map asInt
  |> List.sum
  |> Int

let rec eval (env: Env) (expr: Expr) : Value =
  match expr with
  | Number n -> Int n
  | Symbol s -> env[s]
  | List [] -> failwith "empty application"
  | List (Symbol "quote" :: rest) -> failwith "todo"
  | List (Symbol "define" :: rest) -> failwith "todo"
  | List (Symbol "lambda" :: rest) -> failwith "todo"
  | List (fn :: args) ->
      let f = eval env fn
      let argv = args |> List.map (eval env)
      apply f argv

and apply (f: Value) (args: Value list) : Value =
  match f with
  | Primitive p -> p args
  | Closure _ -> failwith "todo"
  | _ -> failwith "not a function"

let split (s: string) : string[] =
    let ele = s.Replace("(", "").Replace(")", "")
    ele.Split(' ')

let parseToken (t: string) : Expr =
  match System.Int32.TryParse t with
  | true, n -> Number n
  | _ -> Symbol t

let parseSimple (tokens: string[]) : Expr =
  tokens
  |> Array.toList
  |> List.map parseToken
  |> List

let parse (s: string) : Expr =
    let tokens = split s
    parseSimple tokens

let initialEnv : Env =
  Map.ofList [
    "+", Primitive primitiveAdd
  ]

let expr = parse "(+ 1 2 3)"
let result = eval initialEnv expr
printfn "%A" result


