   M S B A M L   `   `   `  ����  Exam_VSTBuh    Exam_VSTBuh.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��131* �6   �� 5	      $
��386* �6   .+�5   
   o� $��20��6   $	��Top=�6   $��2��64   .@�5      m� $��Файл��6   .@�5      m� $��
Выход��6   5      m� 5      $
��Edit��6   6(   m� 5      -   5        $��Справочник��5      .@�5      m� -   5        $��Продавцы��5      6-   m� 5      -   5        $��Бренды��5      6+   m� 5      -   5        $��Товары��5      5      m� 5      -   5        $��Склады��5      6+   5      m� 5      $)��#Складские операции��6   .@�5      m� $��Приход��6   m� 5      $��Расход��6   m� 5      $��Остатки��6   5      5      �� 5   
   $
��Left=�6   $��
101,37,0,0q�6/   $
��Wrap=�6C   $	��Top=�6W   $��49��6o   $	��313��6{   $��30(�6�    �� TextAlignment$ Center=�6�   $��2��6�   �� 6�    � Language$ ru-ru�6�   $%�Добро пожаловать��6�   6�   �� 5!   
   $
��Left=�6   $��
101,86,0,0q�6/   $
��Wrap=�6C   $	��Top=�6W   $��28��6o   $	��313��6{   $��20(�6�   $ Center=�6�   $��2��6�   �� 6�   $ ru-ru�6�   $�Пользователь��6�   6�   �� 5$   
   $
��Left=�6   $��101,119,0,0q�6/   $
��Wrap=�6D   $	��Top=�6X   $��28��6p   $��84��6|   $��20(�6�   $
 Left=�6�   $��2��6�   �� 6�   $ ru-ru�6�   $�Сегодня:��6�   6�   �� 5%   
   -   5         ��Name$ CurDateTblock��5%      $
��Left=�6+   $��131,119,0,0q�6F   $
��Wrap=�6[   $	��Top=�6o   $��28��6�   $	��152��6�   $��20(�6�   $
 Left=�6�   $��//Текущ дата��6�   $��1��6�   �� 5'   
   $
��Left=�6   $��101,152,0,0q�6/   $
��Wrap=�6D   $	��Top=�6X   $��28��6p   $	��313��6|   $��20(�6�   $ Center=�6�   $��2��6�   �� 6�   $ ru-ru�6�   $�
Курсы��6�   6�   �� 5(   
   $
��Left=�6   $��101,201,0,0q�6/   $
��Wrap=�6D   $	��Top=�6X   $��28��6p   $��74��6|   $��20(�6�   $
 Left=�6�   $��2��6�   �� 6�   $ ru-ru�6�   $�Доллар:��6�   6�   �� 5)   
   $
��Left=�6   $��149,201,0,0q�6/   $
��Wrap=�6D   $	��Top=�6X   $��28��6p   $��52��6|   $��20(�6�   $
 Left=�6�   $��1��6�   �� 6�   $ ru-ru�6�   $�	Евро:��6�   6�   �� 5+   
   -   5         ��Name$ DollarCurTBox��5+      $
��Left=�6)   $��
62,201,0,0q�6D    �� TextWrapping$
 Wrap=�6X   $��1��6l   $	��Top=�6u   $��28��6�   $��64��6�    �� TextAlignment$ Center=�6�   $��20(�6�   $��1��6�   �� 5,   
   -   5        $ EuroCurTBox��5,      $
��Left=�6'   $��224,201,0,0q�6B   $
 Wrap=�6W   $��1��6k   $	��Top=�6t   $��28��6�   $��59��6�   $ Center=�6�   $��20(�6�   $��1��6�   �� 5.   
   -	   5         ��Name$ NewSaleButton��5.      $��Новая продажа��6(   $
��Left=�6@   $��101,256,0,0q�6[   $	��Top=�6p   $	��122��6�   $��35��6�   $��2��6�   ��6�   Z� 6�   �� 5/   
   -
   5        $ ReportMakerButton��5/      $��
Отчет��6,   $
��Left=�6<   $��161,256,0,0q�6W   $	��Top=�6l   $	��122��6�   $��35��6�   $��1��6�   ��6�   Z� 6�   52      53      