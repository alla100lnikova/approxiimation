namespace App

type Class1() = 
    member this.X = "F#"

namespace Numerics
module public Process = 
  
  
  //Сумма
  let rec SumX k (xi:double list) =
     if xi=[] then 0.
        else (List.head xi)**float(k) + SumX k (List.tail xi)

  //Соберем строку матрицы, к задаем как степень+номер уравнения
  let rec Str k number (xi:double list) = 
      if k=number then [SumX k xi]
          else (SumX k xi)::(Str (k-1) number xi)
  
  //Соберём матрицу, начальный номер = степени
  let rec Matrix k number xi =
      if number=0 then [Str k 0 xi]
         else (Str (k+number) number xi)::(Matrix k (number-1) xi)
  
  //Сумма
  let rec SumY k (xi:double list) (yi:double list) =
     if xi=[] then double(0)
        else (List.head xi)**double(k)*(List.head yi) + SumY k (List.tail xi) (List.tail yi)

  //Соберем свободный столбец
  let rec Free k xi yi = 
      if k=0 then [SumY k xi yi]
          else (SumY k xi yi)::(Free (k-1) xi yi)
  

  //Приведём матрицу в нормальное состояние
  let rec RevMatrix matrix = 
      if List.tail matrix=[] then [List.rev (List.head matrix)]
          else List.rev (List.head matrix)::RevMatrix(List.tail matrix)

  //Сливаем матрицу и свободный столбец
  let rec FreeSt free matrix =
      if matrix=[] then []
          else ((List.head matrix) @ [(List.head free)])::(FreeSt (List.tail free) (List.tail matrix))
  
//Умножение на коэффициент
  let rec Mult x str=
    if str = [] then []
        else (List.head str * x)::Mult x (List.tail str)

//Вычли строку из строки
  let rec Sub str matrix =
    if matrix=[] then []
        else 
            (List.head matrix - List.head str)::Sub (List.tail str) (List.tail matrix)

//Вычесть первую строку из всех
  let rec SubMatrix str matrix =
    if matrix=[] then []
        else List.tail (Sub (Mult (List.head(List.head matrix)) str) (List.head matrix))::SubMatrix str (List.tail matrix)

//Строку в конец (матрица без головы)

  let rec Swap str matrix =
     matrix @ [str]

//Проверка на строку из нулей (кроме последнего)
  let rec IsNull_row (str:double list) = 
    if List.tail str = [] then true
    elif (List.head str = float(0.)) then IsNull_row (List.tail str)
         else false

//Проверка на столбец из нулей
  let rec IsNull_column (matrix:double list list) = 
    if matrix=[] then true
    elif (List.head(List.head matrix))=float(0.) then  IsNull_column(List.tail matrix)
       else false

//Составим треугольную матрицу
  let rec TriangleM (matrix:double list list) = 
    if 
       (
          (List.tail matrix=[]) && 
          (not ((List.head(List.head matrix))=0.0)) && 
          (not (IsNull_row (List.head matrix)))
       ) 
       then  [(List.head matrix)]
    elif 
        (
             (not ((List.head(List.head matrix))=0.0)) && 
             (not (IsNull_row (List.head matrix))) && 
             (not (IsNull_column matrix))
        ) 
        then 
            (Mult (double(1)/(List.head(List.head matrix))) (List.head matrix)) :: 
            (TriangleM 
               (
                  SubMatrix 
                      (
                         Mult 
                             (double(1)/(List.head(List.head matrix))) 
                             (List.head matrix)
                      ) 
                      (List.tail matrix)
                )
            )
    elif 
        (
            (not (IsNull_row (List.head matrix))) &&  
            (not (IsNull_column matrix))
        ) 
            then TriangleM (Swap (List.head matrix) (List.tail matrix))
        else []

// Подставляет, находит очередной корень
  let rec Root roots str =
    if List.tail str = [] then List.head str
        else  (Root  (List.tail roots) (List.tail str)) - ((List.head roots) * (List.head str))


//Соберем список корней
  let rec AllRoots matrix = 
    if matrix=[] then []
    elif (List.tail matrix = []) then
        if (not ((List.head(List.head matrix))=float(0.))) 
            then ((List.head (List.tail (List.head matrix)))/(List.head(List.head matrix)))::(AllRoots (List.tail matrix))
           else float(0.)::(AllRoots (List.tail matrix))
     else [(Root (AllRoots (List.tail matrix)) (List.tail (List.head matrix)))] @
             (AllRoots (List.tail matrix))
//Основная функция
  let Fun k xi yi =
     let matrix = Matrix k k xi
     let free = List.rev (Free k xi yi)
     let headmatrix = FreeSt free (List.rev(RevMatrix matrix))
     let Tr = TriangleM headmatrix
     if (not (Tr=[]) && (List.length(List.head(List.rev Tr))) = 2) then
           let All = AllRoots (Tr)
           All
        else []

  //Прием данных
  let MyData (k:int) (cxi:double array) (cyi:double array) =
      let xi = Array.toList(cxi) 
      let yi = Array.toList (cyi)
      let My = Fun k xi yi
      if My = [] then List.toArray []
         else List.toArray (My)
      
  


      
  
  

