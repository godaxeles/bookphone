   M S B A M L   `   `   `  ����F  BDeckstopApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DeckstopAppDeckstopApp  5   	       DeckstopApp.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DeckstopApp     5   	     � Title$  	BookPhone��5   	   $	��450��6   $	��800��6(   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5       � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5      .+�5   
   �� $��2��6   ���#007BFF._�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   $��5q�6O   .+�5      �� $��	BookPhone��6   $��30(�6-   
�������$��Center=�6N   5      5      �� 5   
   $��1��6   $��15(�5      ��5      �� *
Contacts+ �� Mode$ OneWay=� �� SelectedItem 5      �� *SelectedContact+$ TwoWay=���5       �� 5!      .R�5"      �� $ 
Horizontal=�6!   .+�5#      �� $��0 0 5 0q�6>   ��6$   �� *
LastName+6>   �� 5$      $��0 0 5 0q�6?   ��6$   �� *	FirstName+6?   �� 5%      ��6$   �� *
MiddleName+5&      5'      5)      � 5,   
   $��1��6   $��1��6    5-      �� 5.      $
��Auto �6    �� 5/      �� 50      $
��Auto �6    .+�53      �� $��5q�6   .+�54      �� $��Фамилия��6   $��15(�6+   �� 55      $��15(�6   $��0 0 0 5q�56      ��57      �� *SelectedContact.LastName+$ TwoWay=�6S   �� 59      $��Имя��6   $��15(�6'   �� 5:      $��15(�6   $��0 0 0 5q�5;      ��5<      �� *SelectedContact.FirstName+$ TwoWay=�6T   �� 5>      $��Отчество��6   $��15(�6,   �� 5?      $��15(�6   $��0 0 0 5q�5@      ��5A      �� *SelectedContact.MiddleName+$ TwoWay=�6U   �� 5C      $��Телефон��6   $��15(�6+   �� 5D      $��15(�6   $��0 0 0 5q�5E      ��5F      �� *SelectedContact.Phone+$ TwoWay=�6P   �� 5H      $��
Адрес��6   $��15(�6)   �� 5I      $��15(�6   $��0 0 0 5q�5J      ��5K      �� *SelectedContact.Address+$ TwoWay=�6R   �� 5M      $��Комментарий��6   $��15(�6/   �� 5N      $��15(�6   $��0 0 0 5q�5O      ��5P      �� *SelectedContact.Description+$ TwoWay=�6V   5R      �� 5T      $��2��6   $ 
Horizontal=�6&   .+�5U      �� $��Сохранить��6   $
��15 5q�6-   $��5q�6<   ���#007BFF
�������$��15(�6o   �� 5V      $��Удалить��6   $
��15 5q�6+   $��5q�6:   ���#dc3545
�������$��15(�6m   5W      5Y      5Z      5[      