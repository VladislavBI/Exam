   M S B A M L   `   `   `  ����[  WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms  5   
   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration 5       Exam_VSTBuh+   %Exam_VSTBuh.DB_Folder.StatisticWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5   
   _wfiOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration     5      -   5        $��
statWindow��5   :     � Title$  StatisticWindow��6	   $	��663��6!   $	��656��6.   .��5      � $��0,0,2,1q�6    � ColumnDefinitions 5   
   �� 5	      $	��10* �6   �� 5
      $��2* �6   .+�5   
   �� -   5         ��Name$ DelBut��5      $��Удалить��6!   $��1��63   $
��Left=�6C   $��
10,174,0,0q�6^   $	��Top=�6r   $��75��6�    J� RenderTransformOrigin$ 0.04,0.2&�6�   $��54��6�   $��15(�6�   6�   �� 5   
   -   5        $ 	ChangeBut��5      $��Изменить��6$   $��1��67   $
��Left=�6G   $��	10,98,0,0q�6b   $	��Top=�6u   $��75��6�   $ 0.04,0.2&�6�   $��54��6�   $��15(�6�   6�   �� 5   
   -   5        $ AddBut��5      $��Добавить��6!   $��1��64   $��Right=�6D   $��	0,22,23,0q�6`   $	��Top=�6s   $��75��6�   $ 0.04,0.2&�6�   $��54��6�   $��15(�6�   6�   � 5   
   -   5        $��dataGridsGrid��5       � RowDefinitions 5      �� 5      �� 5      -   5         �Name$ RowDEfResultRow��5       .+�5      &   System.Windows.Controls.DataGrid  -   5        $��dataGridView��5   $   $��0��6      CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .  $��0,0,0,0q�6�   	   AreRowDetailsFrozen	 . 
   
IsReadOnly
 .    SelectionMode$ Single=�6�     5      -   5        $��dataGridSelect��5   $   $��1��6    .   .   .  $��0,0,0,0q�6�   
 . 6�   5      �� 5   
   -	   5        $ PrevBut��5      $��
Назад��6"   $��1��62   $
��Left=�6B   $��
10,258,0,0q�6]   $	��Top=�6q   $��75��6�   $��54��6�   $��15(�6�   $y�Visible=�6�   6�   �� 5   
   -
   5        $ ButOK��5      $��OK��6    $��1��6-   $
��Left=�6=   $��
10,567,0,0q�6X   $	��Top=�6l   $��75��6�   $��54��6�   $��15(�6�   $y�Visible=�6�   6�   5      5      